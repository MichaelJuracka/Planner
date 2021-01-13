using ExcelDataReader;
using Planner.Business.Interfaces;
using Planner.Data.Interfaces;
using Planner.Data.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;

namespace Planner.Business.Managers
{
    public class OfficeManager : IOfficeManager
    {
        private readonly IPassengerRepository passengerRepository;
        private readonly IStationRepository stationRepository;
        private readonly IExportManager exportManager;
        private readonly IOwnerManager ownerManager;

        public OfficeManager(IPassengerRepository passengerRepository, IStationRepository stationRepository, IExportManager exportManager, IOwnerManager ownerManager)
        {
            this.passengerRepository = passengerRepository;
            this.stationRepository = stationRepository;
            this.exportManager = exportManager;
            this.ownerManager = ownerManager;
        }
        #region Import
        public List<Passenger> ImportPassengers(string filePath, Route route, IEnumerable<Station> boardingStations, IEnumerable<Station> exitStations, ObservableCollection<Owner> owners)
        {
            List<Passenger> passengers = new List<Passenger>();

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        int businessCase;
                        if (reader.GetValue(6) != null)
                            businessCase = int.Parse(reader.GetValue(6).ToString());
                        else
                            throw new ArgumentException($"chyba na řádku! {reader.GetValue(0)}, obchodní případ");
                        string firstName;
                        if (reader.GetValue(3) != null)
                            firstName = reader.GetValue(3).ToString();
                        else
                            firstName = "doplnime";
                        string secondName;
                        if (reader.GetValue(4) != null)
                            secondName = reader.GetValue(4).ToString();
                        else
                            secondName = "doplnime";
                        string phone = null;
                        if (reader.GetValue(5) != null)
                        {
                            phone = reader.GetValue(5).ToString();
                            if (phone.StartsWith("+420"))
                                phone = phone.Substring(4);
                        }
                        string addtionalInformation = null;
                        if (reader.GetValue(8) != null)
                            addtionalInformation = reader.GetValue(8).ToString();

                        var owner = owners.FirstOrDefault(x => x.Name.ToLower() == reader.GetValue(7).ToString().ToLower());
                        if (owner == null)
                        {
                            owner = ownerManager.Add(reader.GetValue(7).ToString(), null);
                            owners.Add(owner);
                        }


                        //string owner;
                        //if (reader.GetValue(7) != null)
                        //    owner = reader.GetValue(7).ToString();

                        if (route == null)
                            throw new ArgumentException("Vyberte jízdu");
                        var boardingStation = boardingStations.SingleOrDefault(x => x.Name.ToLower() == reader.GetValue(1).ToString().ToLower());
                        var exitStation = exitStations.SingleOrDefault(x => x.Name.ToLower() == reader.GetValue(2).ToString().ToLower());
                        if (boardingStation == null)
                            throw new ArgumentException($"chyba na řádku! { reader.GetValue(0) }, nástupní stanice");
                        if (exitStation == null)
                            throw new ArgumentException($"chyba na řádku! {reader.GetValue(0)}, výstupní stanice");

                        var passenger = new Passenger()
                        {
                            BusinessCase = businessCase,
                            FirstName = firstName,
                            SecondName = secondName,
                            Phone = phone,
                            AdditionalInformation = addtionalInformation,
                            RouteId = route.RouteId,
                            IsCleared = false,
                            SeatNumber = null,
                            BoardingStationId = boardingStation.StationId,
                            ExitStationId = exitStation.StationId,
                            //Třeba dodělat - prozatím nelze získat z exportu
                            Email = null,
                            BoardingRouteId = null,
                            OwnerId = owner.OwnerId
                        };
                        if (!route.RouteBack)
                            passenger.DepartureTime = boardingStation.DepartureTime;
                        else
                            passenger.DepartureTime = null;

                        passengers.Add(passenger);
                    }
                }
            }
            foreach (var p in passengers)
            {
                passengerRepository.Insert(p);
            }

            return passengers;
        }
        public List<Station> ImportStations(string filePath, IEnumerable<Region> regions)
        {
            var stations = new List<Station>();

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        string name;
                        if (reader.GetValue(1) != null)
                            name = reader.GetValue(1).ToString();
                        else
                            throw new ArgumentException($"Chyba na řádku! {reader.GetValue(0)}, název stanice");
                        string departureTime;
                        if (reader.GetValue(2) != null)
                        {
                            departureTime = reader.GetValue(2).ToString().Substring(reader.GetValue(2).ToString().Length - 8);
                            departureTime = departureTime.Substring(0, 5);
                        }
                        else
                            departureTime = null;
                        string departurePlace;
                        if (reader.GetValue(3) != null)
                            departurePlace = reader.GetValue(3).ToString();
                        else
                            throw new ArgumentException($"Chyba na řádku! { reader.GetValue(0) }, odjezdové místo");
                        string description;
                        if (reader.GetValue(4) != null)
                            description = reader.GetValue(4).ToString();
                        else
                            description = null; string gps;
                        if (reader.GetValue(5) != null)
                            gps = reader.GetValue(5).ToString();
                        else
                            gps = null;
                        bool boardingStation = false;
                        if (reader.GetValue(6) != null)
                        {
                            if (reader.GetValue(6).ToString().ToLower() == "true")
                            {
                                boardingStation = true;
                            }
                            else if (reader.GetValue(6).ToString().ToLower() == "false")
                            {
                                boardingStation = false;
                            }
                            else
                                throw new ArgumentException($"Chyba na řádku! {reader.GetValue(0)}, nástupní stanice");

                        }
                        var region = regions.SingleOrDefault(x => x.Name.ToLower() == reader.GetValue(7).ToString().ToLower());
                        if (region == null)
                            throw new ArgumentException($"Chyba na řádku! { reader.GetValue(0) }, oblast");

                        var station = new Station()
                        {
                            Name = name,
                            DepartureTime = departureTime,
                            DeparturePlace = departurePlace,
                            Description = description,
                            Gps = gps,
                            BoardingStation = boardingStation,
                            RegionId = region.RegionId
                        };
                        stations.Add(station);
                    }
                }
            }
            foreach (var s in stations)
            {
                stationRepository.Insert(s);
            }

            return stations;
        }
        #endregion
        #region Export
        public void RouteList(IEnumerable<Passenger> passengers, string filePath, int routeId, string fileName, bool isRealRoute)
        {
            var excelApp = new Excel.Application
            {
                Visible = false
            };

            excelApp.Workbooks.Add();

            Excel._Worksheet worksheet = (Excel.Worksheet)excelApp.ActiveSheet;

            if (!isRealRoute)
            {
                worksheet.Cells[1, "A"] = "#";
                worksheet.Cells[1, "B"] = "Bus";
                worksheet.Cells[1, "C"] = "Jméno";
                worksheet.Cells[1, "D"] = "Přijmení";
                worksheet.Cells[1, "E"] = "Telefon";
                worksheet.Cells[1, "F"] = "OP";
                worksheet.Cells[1, "G"] = "Vlastník";
                worksheet.Cells[1, "H"] = "Výstupní stanice";
                worksheet.Cells[1, "I"] = "Nástupní stanice";
                worksheet.Cells[1, "J"] = "Poznámka";
            }
            else
            {
                worksheet.Cells[1, "A"] = "#";
                worksheet.Cells[1, "B"] = "Jméno";
                worksheet.Cells[1, "C"] = "Přijmení";
                worksheet.Cells[1, "D"] = "Telefon";
                worksheet.Cells[1, "E"] = "OP";
                worksheet.Cells[1, "F"] = "Vlastník";
                worksheet.Cells[1, "G"] = "Výstupní stanice";
                worksheet.Cells[1, "H"] = "Nástupní stanice";
                worksheet.Cells[1, "I"] = "Poznámka";
            }

            var row = 1;

            if (!isRealRoute)
            {
                foreach (var p in passengers)
                {
                    worksheet.Cells[row + 1, "A"] = row;
                    row++;
                    if (p.RealRoute == null)
                        worksheet.Cells[row, "B"] = "";
                    else
                        worksheet.Cells[row, "B"] = p.RealRoute.LicensePlate;
                    worksheet.Cells[row, "C"] = p.FirstName;
                    worksheet.Cells[row, "D"] = p.SecondName;
                    worksheet.Cells[row, "E"] = p.Phone;
                    worksheet.Cells[row, "F"] = p.BusinessCase;
                    worksheet.Cells[row, "G"] = p.Owner.ToString();
                    worksheet.Cells[row, "H"] = p.ExitStation.ToString();
                    worksheet.Cells[row, "I"] = p.BoardingStation.ToString();
                    worksheet.Cells[row, "J"] = p.AdditionalInformation;
                }
            }
            else
            {
                var stations = new List<Station>();
                int rowNumber = 1;

                foreach (var p in passengers)
                {
                    if (!stations.Contains(p.ExitStation))
                        stations.Add(p.ExitStation);
                }

                foreach (var s in stations)
                {
                    row++;
                    worksheet.Cells[row, "A"] = $"{passengers.Where(x => x.ExitStationId == s.StationId).Count()}pax, {s.Name}, {s.DeparturePlace}, {s.Gps}";
                    row++;
                    worksheet.Cells[row, "A"] = s.Description;

                    Excel.Range firstRow = worksheet.Range[worksheet.Cells[row - 1, "A"], worksheet.Cells[row - 1, "I"]];
                    Excel.Range secondRow = worksheet.Range[worksheet.Cells[row, "A"], worksheet.Cells[row, "I"]];

                    firstRow.Merge();
                    firstRow.Font.Bold = true;
                    secondRow.Merge();
                    secondRow.Font.Bold = true;

                    foreach (var p in passengers.Where(x => x.ExitStationId == s.StationId))
                    {
                        row++;
                        worksheet.Cells[row, "A"] = rowNumber;
                        rowNumber++;
                        worksheet.Cells[row, "B"] = p.FirstName;
                        worksheet.Cells[row, "C"] = p.SecondName;
                        worksheet.Cells[row, "D"] = p.Phone;
                        worksheet.Cells[row, "E"] = p.BusinessCase;
                        worksheet.Cells[row, "F"] = p.Owner.ToString();
                        worksheet.Cells[row, "G"] = p.ExitStation.ToString();
                        worksheet.Cells[row, "H"] = p.BoardingStation.ToString();
                        worksheet.Cells[row, "I"] = p.AdditionalInformation;
                    }
                }
            }

            Excel.Range range;
            Excel.Range headline;

            if (!isRealRoute)
            {
                range = worksheet.Range[worksheet.Cells[1, "A"], worksheet.Cells[row, "J"]];
                headline = worksheet.Range[worksheet.Cells[1, "A"], worksheet.Cells[1, "J"]];
            }
            else
            {
                range = worksheet.Range[worksheet.Cells[1, "A"], worksheet.Cells[row, "I"]];
                headline = worksheet.Range[worksheet.Cells[1, "A"], worksheet.Cells[1, "I"]];
            }
            foreach (Excel.Range cell in range.Cells)
                cell.BorderAround2();

            headline.Font.Bold = true;
            Excel.Borders headlineBoarder = headline.Borders;
            headlineBoarder[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlDouble;

            for (int i = 0; i < 9; i++)
            {
                worksheet.Columns[i + 1].AutoFit();
            }

            worksheet.SaveAs(filePath);

            excelApp.Workbooks.Close();
            excelApp.Quit();

            var file = File.ReadAllBytes(filePath);

            exportManager.Add(file, fileName + ".xlsx", routeId);
        }
        public void UpdateOrder(string filePath, string fileName, Route route, IEnumerable<Passenger> passengers, IEnumerable<Passenger> passengersBack)
        {
            string templatePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            templatePath += @"\Templates\orderExcelTemplate.xls";

            var excelApp = new Excel.Application();

            var excelbook = excelApp.Workbooks.Open(templatePath);

            Excel._Worksheet worksheet = (Excel.Worksheet)excelApp.Sheets["objednávka"];

            //základní hodnoty
            //Datum odjezdu
            worksheet.Cells[1, "F"] = route.DepartureDate.ToShortDateString().Substring(0, 5) + " - " + route.DepartureDate.AddDays(2).ToShortDateString().Substring(0, 5);
            //Typ busu
            worksheet.Cells[4, "F"] = "N" + route.BusType.NumberOfSeats.ToString();
            //Destinace
            worksheet.Cells[2, "K"] = route.Region.ToString();
            Excel.Range region = worksheet.Range[worksheet.Cells[1, "J"], worksheet.Cells[2, "K"]];
            region.Merge();
            region.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //dopravce
            worksheet.Cells[11, "C"] = route.Provider.ToString() + " " + route.LicensePlate;
            //počet cestujících tam
            worksheet.Cells[14, "F"] = passengers.Count();
            //počet cestujících zpět
            worksheet.Cells[14, "H"] = passengersBack.Count();

            //datum u prvního řádku
            worksheet.Cells[19, "B"] = route.DepartureDate.ToShortDateString().Substring(0, 5) + ".";

            //Hlavní nástupní místo (Brno + svozy kromě břeclavské a slovenské linky)
            worksheet.Cells[19, "C"] = "Brno, UAN Zvonařka /" + passengers.Where(x => x.BoardingStation.RegionId != 19 && x.BoardingStation.RegionId != 20).Count() + "/";
            worksheet.Cells[19, "K"] = passengers.FirstOrDefault(x => x.BoardingStationId == 98).DepartureTime;
            worksheet.Range[worksheet.Cells[19, "C"], worksheet.Cells[19, "H"]].Merge();

            //Druhé nástupní místo (Břeclav + linka)
            if (passengers.FirstOrDefault(x => x.BoardingStationId == 99) != null)
            {
                worksheet.Cells[20, "K"] = passengers.FirstOrDefault(x => x.BoardingStationId == 99).DepartureTime;
            }
            else
                worksheet.Cells[20, "K"] = "18:55";
            worksheet.Cells[20, "C"] = "Břeclav, Čerpací stanice Benzina, ul. Lidická /" + passengers.Where(x => x.BoardingStation.RegionId == 19).Count() + "/";
            worksheet.Range[worksheet.Cells[20, "C"], worksheet.Cells[20, "H"]].Merge();

            //Třetí nástupní místo (Bratislava + linka)
            if (passengers.FirstOrDefault(x => x.BoardingStationId == 102) != null)
            {
                worksheet.Cells[21, "K"] = passengers.FirstOrDefault(x => x.BoardingStationId == 102).DepartureTime;
            }
            else
                worksheet.Cells[21, "K"] = "19:50";
            worksheet.Cells[21, "C"] = "Bratislava, Autobusová zastávka u ZOO, na dálnici D2 /" + passengers.Where(x => x.BoardingStation.RegionId == 20).Count() + "/";
            worksheet.Range[worksheet.Cells[21, "C"], worksheet.Cells[21, "H"]].Merge();

            //Výstupní místa tam
            var exitStations = new List<Station>();

            foreach (var p in passengers)
            {
                if (!exitStations.Contains(p.ExitStation))
                {
                    exitStations.Add(p.ExitStation);
                }
            }

            int row = 25;
            foreach (var s in exitStations)
            {
                if (row != 25)
                {
                    Excel.Range line = worksheet.Rows[row].EntireRow;
                    line.Insert(Excel.XlDirection.xlDown);
                }
                else
                {
                    worksheet.Cells[row, "B"] = route.DepartureDate.AddDays(1).ToShortDateString().Substring(0, 5) + ".";
                }
                worksheet.Cells[row, "C"] = s.ToString() + " /" + passengers.Where(x => x.ExitStationId == s.StationId).Count() + "/";

                Excel.Range exitStationRange = worksheet.Range[worksheet.Cells[row, "C"], worksheet.Cells[row, "H"]];
                exitStationRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                exitStationRange.Font.Bold = true;
                exitStationRange.Merge();

                row++;
            }

            //Přidání prázdných řádků
            for (int i = 0; i < 3; i++)
            {
                Excel.Range line = worksheet.Rows[row + i].EntireRow;
                line.Insert(Excel.XlDirection.xlDown);
                if (i == 1)
                    worksheet.Cells[row + i, "C"] = "devítka v ";
                if (i != 2)
                    worksheet.Range[worksheet.Cells[row + i, "C"], worksheet.Cells[row + i, "H"]].Merge();
            }

            //výstupní místa Zpět
            var exitStationsBack = new List<Station>();

            foreach (var p in passengersBack)
            {
                if (!exitStationsBack.Contains(p.ExitStation))
                {
                    exitStationsBack.Add(p.ExitStation);
                }
            }

            foreach (var s in exitStationsBack)
            {

                Excel.Range line = worksheet.Rows[row + 3].EntireRow;
                line.Insert(Excel.XlDirection.xlDown);

                worksheet.Cells[row + 3, "C"] = s.ToString() + " /" + passengersBack.Where(x => x.ExitStationId == s.StationId).Count() + "/";

                Excel.Range exitStationBackRange = worksheet.Range[worksheet.Cells[row + 3, "C"], worksheet.Cells[row + 3, "H"]];
                exitStationBackRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                exitStationBackRange.Font.Bold = true;
                exitStationBackRange.Merge();

                //Zde přidat odjezdové časy z HR!!

                row++;
            }

            //Nástupní místa na cestu zpět
            worksheet.Cells[row + 27, "B"] = route.DepartureDate.AddDays(2).ToShortDateString().Substring(0, 5) + ".";

            worksheet.Cells[row + 27, "C"] = "Bratislava, Autobusová zastávka u ZOO, na dálnici D2 /" + passengers.Where(x => x.BoardingStation.RegionId == 20).Count() + "/";
            worksheet.Range[worksheet.Cells[row + 27, "C"], worksheet.Cells[row + 27, "H"]].Merge();

            worksheet.Cells[row + 28, "C"] = "Břeclav, Čerpací stanice Benzina, ul. Lidická /" + passengers.Where(x => x.BoardingStation.RegionId == 19).Count() + "/";
            worksheet.Range[worksheet.Cells[row + 28, "C"], worksheet.Cells[row + 28, "H"]].Merge();

            worksheet.Cells[row + 29, "C"] = "Brno, UAN Zvonařka /" + passengers.Where(x => x.BoardingStation.RegionId != 19 && x.BoardingStation.RegionId != 20).Count() + "/";
            Excel.Range departureTimeCell = worksheet.Cells[row + 29, "K"];
            worksheet.Cells[row + 29, "K"] = "9:00";
            departureTimeCell.Font.Bold = true;
            departureTimeCell.Font.Size = 12;
            departureTimeCell.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            worksheet.Range[worksheet.Cells[row + 29, "C"], worksheet.Cells[row + 29, "H"]].Merge();

            //bez pop-up oken
            excelApp.DisplayAlerts = false;

            excelbook.SaveAs(filePath);
            excelbook.Close();
            excelApp.Quit();

            var file = File.ReadAllBytes(filePath);

            exportManager.Add(file, fileName + ".xls", route.RouteId);
        }
        public void UpdateOrderWord(string filePath, string fileName, Route route, IEnumerable<Passenger> passengers)
        {
            string templatePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            templatePath += @"\Templates\orderWordSvozTemplate.doc";
            if (route.RouteBack)
                templatePath += @"\Templates\orderWordRozvozTemplate.doc";

            var wordApp = new Word.Application();
            wordApp.Visible = false;
            wordApp.DisplayAlerts = Word.WdAlertLevel.wdAlertsNone;
            var document = wordApp.Documents.Open(templatePath);

            string provider = route.Provider.ToString();
            string departureDate = route.DepartureDate.ToShortDateString();
            if (route.RouteBack)
                departureDate = route.DepartureDate.AddDays(1).ToShortDateString();
            string passengerCount = passengers.Count().ToString();

            var stations = new List<Station>();
            var businessCases = new List<int>();

            foreach(var p in passengers)
            {
                if (!stations.Contains(p.BoardingStation))
                {
                    stations.Add(p.BoardingStation);
                }
                if (!businessCases.Contains(p.BusinessCase))
                {
                    businessCases.Add(p.BusinessCase);
                }
            }

            string routeText = "";
            if (!route.RouteBack)
            {
                foreach (var s in stations)
                {
                    routeText += $"{passengers.FirstOrDefault(x => x.BoardingStationId == s.StationId).DepartureTime} {passengers.Where(x => x.BoardingStationId == s.StationId).Count()}pax {s.Name}, {s.DeparturePlace}, GPS: {s.Gps}\n";
                    foreach (var b in businessCases)
                    {
                        if (passengers.FirstOrDefault(x => x.BusinessCase == b).BoardingStationId == s.StationId)
                        {
                            var passenger = passengers.FirstOrDefault(x => x.BusinessCase == b && x.Phone != null);
                            string phone = "";
                            if (passenger != null)
                                phone = passenger.Phone;

                            routeText += $"{passengers.Where(x => x.BusinessCase == b).Count()}x {passengers.FirstOrDefault(x => x.BusinessCase == b).SecondName}, tel. {phone}\n";
                        }
                    }
                    routeText += "\n";
                }
            }
            else
            {
                foreach (var s in stations)
                {
                    routeText += $"{passengers.Where(x => x.BoardingStationId == s.StationId).Count()}x {s.Name}, {s.DeparturePlace}, GPS: {s.Gps}\n\n";
                }
            }

            WordFindAndReplace(wordApp, "<dopravce>", provider);
            WordFindAndReplace(wordApp, "<datum>", departureDate);
            WordFindAndReplace(wordApp, "<pocetOsob>", passengerCount);
            Word.Range range = document.Content;
            range.Find.Execute("<svoz>");
            range.Text = routeText;

            document.SaveAs(filePath);
            document.Close();
            wordApp.Quit();

            var file = File.ReadAllBytes(filePath);

            exportManager.Add(file, fileName + ".doc", route.RouteId);
        }
        private void WordFindAndReplace(Word.Application wordApp, object findText, object replaceText)
        {
            object matchCase = true;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundsLike = false;
            object matchAllWordForms = false;
            object forward = true;
            object format = false;
            object matchKashida = false;
            object matchDiacritics = false;
            object matchAlefHamza = false;
            object matchControl = false;
            object replace = 2;
            object wrap = 1;
            wordApp.Selection.Find.Execute(ref findText, ref matchCase,
                ref matchWholeWord, ref matchWildCards, ref matchSoundsLike,
                ref matchAllWordForms, ref forward, ref wrap, ref format,
                ref replaceText, ref replace, ref matchKashida,
                        ref matchDiacritics,
                ref matchAlefHamza, ref matchControl);
        }
        #endregion
    }
}
