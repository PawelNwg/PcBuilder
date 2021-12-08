using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PcBuilder.Interfaces;
using PcBuilder.Models;
using PcBuilder.Services.Configurator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PcBuilder.ViewComponents
{
    public class ConfiguratorValidatorViewComponent : ViewComponent
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private ConfiguratorManager _configuratorManager;
        private List<string> _validatorMessages = new();
        private List<List<DetailedDataProduct>> detailedDataProducts = new();
        private List<ConfiguratorPosition> _configuration;
        private List<Component> components = new();

        // move to enum
        private const string MEMORY = "Pamięć";

        private const string GPU = "GPU";
        private const string CASE = "Obudowy";
        private const string RAM = "RAM";
        private const string MOTHERBOARD = "Płyty główne";
        private const string CPU = "CPU";
        private const string PSU = "Zasilacze";
        private const string COOLING = "Chłodzenie";

        public ConfiguratorValidatorViewComponent(IRepositoryWrapper repositoryWrapper, IHttpContextAccessor httpContextAccessor)
        {
            _configuratorManager = new ConfiguratorManager(_repositoryWrapper, httpContextAccessor);
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            await ValidateConfiguration();
            return await Task.FromResult((IViewComponentResult)View("ConfiguratorValidator", _validatorMessages));
        }

        private async Task<bool> ValidateConfiguration()
        {
            _configuration = _configuratorManager.GetConfiguration();
            if (_configuration is null)
                return false;

            if (!ValidateQuantityOfCategoryConfiguration())
                return false;

            foreach (var item in _configuration)
            {
                var detailedDataProduct = await _repositoryWrapper.RepositoryDetailedDataProduct.GetByCondition(x => x.ProductId == item.product.ProductId);
                if (detailedDataProduct is not null)
                    components.Add(new Component() { Product = item.product, Category = item.category, DetailedDataProduct = detailedDataProduct });
            }
            List<Category> categories = _repositoryWrapper.RepositoryCategory.GetAll().Result;
            List<Category> categoriesInConfiguration = new List<Category>();
            _configuration.ForEach(x => categoriesInConfiguration.Add(x.category));
            categoriesInConfiguration.ForEach(y => categories.RemoveAll(x => x.Name == y.Name));

            foreach (var category in categories)
            {
                switch (category)
                {
                    case { Name: MEMORY }:
                        _validatorMessages.Add($"W konfiguracji brakuje pamięci masowej");
                        break;

                    case { Name: GPU }:
                        _validatorMessages.Add($"W konfiguracji brakuje karty graficznej");
                        break;

                    case { Name: CASE }:
                        _validatorMessages.Add($"W konfiguracji brakuje obudowy");
                        break;

                    case { Name: RAM }:
                        _validatorMessages.Add($"W konfiguracji brakuje pamięci RAM");
                        break;

                    case { Name: MOTHERBOARD }:
                        _validatorMessages.Add($"W konfiguracji brakuje płyty głównej");
                        break;

                    case { Name: CPU }:
                        _validatorMessages.Add($"W konfiguracji brakuje procesora");
                        break;

                    case { Name: PSU }:
                        _validatorMessages.Add($"W konfiguracji brakuje zasilacza");
                        break;

                    case { Name: COOLING }:
                        _validatorMessages.Add($"W konfiguracji brakuje chłodzenia");
                        break;

                    default:
                        break;
                }
            }

            foreach (var configuratorPosition in _configuration)
            {
                switch (configuratorPosition.category)
                {
                    case { Name: MEMORY }:
                        ValidateMemory();
                        break;

                    case { Name: GPU }:
                        ValidateGPU();
                        break;

                    case { Name: CASE }:
                        ValidatePcCase();
                        break;

                    case { Name: RAM }:
                        ValidateRAM();
                        break;

                    case { Name: MOTHERBOARD }:
                        ValidateMotherBoard();
                        break;

                    case { Name: CPU }:
                        ValidateCpu();
                        break;

                    case { Name: PSU }:
                        break;

                    case { Name: COOLING }:
                        ValidateCooling();
                        break;

                    default:
                        break;
                }
            }
            return true;
        }

        private bool ValidateCpu()
        {
            var cpu = components?.Where(x => x.Category.Name == CPU).FirstOrDefault();
            var cpuPowerNeeded = Int16.Parse(cpu?.DetailedDataProduct?.Where(x => x.Name == "PowerNeeded").FirstOrDefault().Value);
            var cpuFamily = cpu?.DetailedDataProduct?.Where(x => x.Name == "CpuFamily").FirstOrDefault().Value;

            var isCpuPowerNeededOK = ValidateCpuPowerNeeded(cpuPowerNeeded);
            var isCpuFamilyOK = ValidateCpuFamily(cpuFamily);
            if (isCpuPowerNeededOK && isCpuFamilyOK)
                return true;
            return false;
        }

        private bool ValidateCpuFamily(string cpuFamily)
        {
            var supportedCpuFamilies = components.Where(x => x.Category?.Name == MOTHERBOARD).FirstOrDefault().DetailedDataProduct?.Where(x => x.Name == "SupportedCpuFamilies").FirstOrDefault().Value;

            if (supportedCpuFamilies is null)
            {
                _validatorMessages.Add($"Może wystąpić problem zgodności procesora z płytą główną");
                return false;
            }

            List<string> supportedCpuFamiliesList = supportedCpuFamilies.Split(new char[] { ';' }).ToList();

            if (!supportedCpuFamilies.Contains(cpuFamily))
            {
                _validatorMessages.Add($"Może wystąpić problem zgodności procesora z płytą główną");
                return false;
            }

            return true;
        }

        private bool ValidateCpuPowerNeeded(int cpuPowerNeeded)
        {
            var powersupplyValue = components?.Where(x => x.Category?.Name == PSU).FirstOrDefault().DetailedDataProduct?.Where(x => x?.Name == "MaxPower").FirstOrDefault().Value;

            if (powersupplyValue is null)
            {
                //_validatorMessages.Add($"Procesor nie będzie działać bez zasilacza");
                return false;
            }

            var powersupplyPowerInt = Int16.Parse(powersupplyValue);

            if (powersupplyPowerInt < cpuPowerNeeded)
            {
                _validatorMessages.Add($"Komputer potrzebuje większego zasilacza");
                return false;
            }

            return true;
        }

        private void ValidateMotherBoard()
        {
        }

        private bool ValidatePcCase()
        {
            var pccase = components?.Where(x => x.Category?.Name == CASE).FirstOrDefault();
            var pccaseMotherBoardStandard = pccase?.DetailedDataProduct?.Where(x => x?.Name == "MotherBoardStandard").FirstOrDefault().Value;

            if (ValidateMotherBoardStandard(pccaseMotherBoardStandard))
                return true;
            return false;
        }

        private bool ValidateMotherBoardStandard(string pccaseMotherBoardStandard)
        {
            var motherboardStandard = components?.Where(x => x.Category?.Name == MOTHERBOARD).FirstOrDefault();
            if (motherboardStandard.Value.Category is null)
            {
                //_validatorMessages.Add($"W konfiguracji brakuje płyty głównej");
                return false;
            }

            var motherboardStandardValue = motherboardStandard?.DetailedDataProduct?.Where(x => x?.Name == "MotherBoardStandard").FirstOrDefault().Value;

            if (pccaseMotherBoardStandard != motherboardStandardValue)
            {
                _validatorMessages.Add($"Standard płyty głównej nie pasuje do wybranej obudowy");
                return false;
            }
            return true;
        }

        private bool ValidateCooling()
        {
            var cooling = components?.Where(x => x.Category.Name == COOLING).FirstOrDefault();
            if (cooling is null)
                return false;
            var coolingLenght = Int16.Parse(cooling?.DetailedDataProduct?.Where(x => x?.Name == "Length").FirstOrDefault().Value);

            if (ValidateCoolingLength(coolingLenght))
                return true;
            return false;
        }

        private bool ValidateCoolingLength(int coolingLenght)
        {
            var pccase = components?.Where(x => x.Category.Name == CASE).FirstOrDefault();
            if (pccase is null)
            {
                //_validatorMessages.Add($"W zestawie brakuje obudowy");
                return false;
            }
            var pccaseMaxCoolingHeight = Int16.Parse(pccase?.DetailedDataProduct?.Where(x => x.Name == "MaxCoolingHeight").FirstOrDefault().Value);

            if (coolingLenght > pccaseMaxCoolingHeight)
            {
                _validatorMessages.Add($"Chłodzenie nie zmieści się w obudowie");
                return false;
            }
            return true;
        }

        //private bool ValidateMemory()
        //{
        //    var memory = components?.Where(x => x.Category.Name == MEMORY).FirstOrDefault();
        //    var memorySata = memory?.DetailedDataProduct.Where(x => x.Name == "SATA").FirstOrDefault().Value;

        //    if (ValidateMemorySata(memorySata))
        //        return true;
        //    return false;
        //}

        //private bool ValidateMemorySata(string memorySata)
        //{
        //    var motherboardSata = components.Where(x => x.Category.Name == MOTHERBOARD).FirstOrDefault().DetailedDataProduct.Where(x => x.Name == "SATA").FirstOrDefault().Value;

        //    if (memorySata != motherboardSata)
        //    {
        //        _validatorMessages.Add($"Standary SATA dysku oraz płyty glównej różnią się");
        //        return false;
        //    }
        //    return true;
        //}

        private void ValidateMemory()
        {
        }

        private bool ValidateRAM()
        {
            var ram = components.Where(x => x.Category.Name == RAM).FirstOrDefault();
            var ramClockSpeed = ram.DetailedDataProduct.Where(x => x.Name == "ClockSpeed").FirstOrDefault().Value;
            var ramStandard = ram.DetailedDataProduct.Where(x => x.Name == "RamStandard").FirstOrDefault().Value;

            var isRamClockOk = ValidateRamClockSpeed(ramClockSpeed);
            var isRamStandardOk = ValidateRamStandard(ramStandard);

            if (isRamClockOk
            &&
            isRamStandardOk)
                return true;
            return false;
        }

        private bool ValidateRamStandard(string ramStandard)
        {
            var motherboardRamStandardValue = components?.Where(x => x.Category?.Name == MOTHERBOARD).FirstOrDefault().DetailedDataProduct?.Where(x => x?.Name == "RamStandard").FirstOrDefault().Value;

            if (motherboardRamStandardValue is null)
            {
                _validatorMessages.Add($"Pamięć RAM wymaga płyty głównej");
                return false;
            }

            if (ramStandard != motherboardRamStandardValue)
            {
                _validatorMessages.Add($"Płyta główna nie obsługuje tego standardu pamięci RAM");
                return false;
            }

            return true;
        }

        private bool ValidateRamClockSpeed(string clockSpeed)
        {
            var motherboardRamClockSpeedValue = components?.Where(x => x.Category?.Name == MOTHERBOARD).FirstOrDefault().DetailedDataProduct?.Where(x => x?.Name == "ClockSpeed").FirstOrDefault().Value;
            if (motherboardRamClockSpeedValue is null)
            {
                _validatorMessages.Add($"Pamięć RAM wymaga płyty głównej");
                return false;
            }
            List<string> compatibileClockSpeeds = motherboardRamClockSpeedValue.Split(new char[] { ';' }).ToList();

            if (!compatibileClockSpeeds.Contains(clockSpeed))
            {
                _validatorMessages.Add($"Płyta głowna nie akceptuje tego standardu częstotliwości pamięci RAM");
                return false;
            }

            return true;
        }

        private bool ValidateQuantityOfCategoryConfiguration()
        {
            List<Category> categories = new();
            foreach (var item in _configuration)
            {
                if (!categories.Any(x => x.CategoryId == item.category.CategoryId))
                    categories.Add(item.category);
                else
                {
                    _validatorMessages.Add($"Dodano wiecej niż jeden komponent z kategorii {item.category}, walidacja niemożliwa");
                    return false;
                }
            }
            return true;
        }

        private bool ValidateGPU()
        {
            var gpu = components.Where(x => x.Category?.Name == GPU).FirstOrDefault();
            var gpuLength = Int16.Parse(gpu.DetailedDataProduct?.Where(x => x?.Name == "Length").FirstOrDefault().Value);
            var gpuEfficiency = Int16.Parse(gpu.DetailedDataProduct?.Where(x => x?.Name == "Efficiency").FirstOrDefault().Value);
            var gpuPowerNeeded = Int16.Parse(gpu.DetailedDataProduct.Where(x => x?.Name == "PowerNeeded").FirstOrDefault().Value);

            var isGPUsLengthOk = ValidateGPUsLength(gpuLength);
            var isGPUsEfficiencyOk = ValidateGPUsEfficiency(gpuEfficiency);
            var isGPUsPowerNeededOk = ValidateGPUsPowerNeeded(gpuPowerNeeded);

            if (isGPUsLengthOk && isGPUsEfficiencyOk && isGPUsPowerNeededOk)            
                return true;
            return false;
        }

        private bool ValidateGPUsPowerNeeded(int gpuPowerNeeded)
        {
            var powersupplyValue = components?.Where(x => x.Category?.Name == PSU).FirstOrDefault().DetailedDataProduct?.Where(x => x?.Name == "MaxPower").FirstOrDefault().Value;

            if (powersupplyValue is null)
            {
                _validatorMessages.Add($"Karta graficzna nie będzie działać bez zasilacza");
                return false;
            }

            var powersupplyPowerInt = Int16.Parse(powersupplyValue);

            if (powersupplyPowerInt < gpuPowerNeeded)
            {
                _validatorMessages.Add($"Komputer potrzebuje większego zasilacza");
                return false;
            }

            return true;
        }

        private bool ValidateGPUsEfficiency(int gpuEfficiency)
        {
            var cpuValue = components?.Where(x => x.Category?.Name == CPU).FirstOrDefault().DetailedDataProduct?.Where(x => x?.Name == "Efficiency").FirstOrDefault().Value;

            if (cpuValue is null)
            {
                _validatorMessages.Add($"Karta graficzna nie będzie działać bez procesora");
                return false;
            }

            var cpuValueInt = Int16.Parse(cpuValue);

            if (cpuValueInt == 1 && gpuEfficiency == 5)
            {
                _validatorMessages.Add($"Karta graficzna jest zbyt wydajna w stosunku do procesora");
                return false;
            }

            if (cpuValueInt == 5 && gpuEfficiency == 1)
            {
                _validatorMessages.Add($"Procesor jest zbyt wydajny w stosunku do karta graficznej");
                return false;
            }

            return true;
        }

        private bool ValidateGPUsLength(int gpuLength)
        {
            var pccaseValue = components?.Where(x => x.Category?.Name == CASE).FirstOrDefault().DetailedDataProduct?.Where(x => x?.Name == "MaxGpuLenght").FirstOrDefault().Value;

            if (pccaseValue is null)
            {
                _validatorMessages.Add($"Karta graficzna potrzebuje obudowy");
                return false;
            }

            var pccaseInt = Int16.Parse(pccaseValue);

            if (gpuLength > pccaseInt)
            {
                _validatorMessages.Add($"Karta graficzna jest zbyt duża do wybranej obudowy");
                return false;
            }

            return true;
        }

        private struct Component
        {
            public Product Product { get; set; }
            public Category Category { get; set; }
            public List<DetailedDataProduct> DetailedDataProduct { get; set; }
        }
    }
}