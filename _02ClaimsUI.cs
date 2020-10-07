using _02_ClaimsRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace _02ClaimsUI
{
    public class ClaimsUI
    {
        private readonly ClaimsRepo _repo = new ClaimsRepo();
        public void Run()
        {
            SeedContent();
            RunMenu();
        }

        public void RunMenu()
        {
            bool continueToRun = (true);

            while (continueToRun)
            {              
                Console.WriteLine("1) View All Claims");
                Console.WriteLine("2) Enter New Claim");
                Console.WriteLine("3) Take Care of Next Claim");
                Console.WriteLine("4) Exit");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ShowClaims();
                        break;
                    case "2":
                        AddClaim();
                        break;
                    case "3":
                        TakeCareOfClaim();
                        break;
                    case "4":
                        continueToRun = false;
                        break;
                    default:
                        continueToRun = false;
                        break;
                }
            }
        }

        public void ShowClaims()
        {
            Console.Clear();
            List<Claims> claims = _repo.GetAllClaims();

            foreach (Claims claim in claims)
            {
                String s = String.Format("{0, -10} {1, -10} {2, -25} {3, -10} {4, -25} {5, -25} {6, -25}\n\n", "ID", "Type", "Description", "Amount", "Incident Date", "Claim Date", "Valid");
                s += String.Format("{0, -10} {1, -10} {2, -25} {3, -10} {4, -25} {5, -25} {6, -35}\n", claim.ClaimID, claim.TypeOfClaim, claim.Description, claim.ClaimAmount, claim.DateofIncident, claim.DateOfClaim, claim.IsValid);
                Console.WriteLine($"\n{s}");
                string line = new String('-', 120);
                Console.WriteLine(line);
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public void TakeCareOfClaim()
        {
            List<Claims> claims = _repo.GetAllClaims();
            bool getNextClaim = true;

            while (getNextClaim)
            {
                Console.WriteLine($"ID: {claims[0].ClaimID}");
                Console.WriteLine($"Type: {claims[0].TypeOfClaim}");
                Console.WriteLine($"Description: {claims[0].Description}");
                Console.WriteLine($"Amount: ${claims[0].ClaimAmount}");
                Console.WriteLine($"Date of incident:{claims[0].DateofIncident}");
                Console.WriteLine($"Date of claim: {claims[0].DateOfClaim}");
                Console.WriteLine($"Valid: {claims[0].IsValid}");

                Console.WriteLine("Take care of this claim? Yes or No");
                string input = Console.ReadLine();

                if (input == "Yes" || input == "yes" || input == "y" || input == "Y")
                {
                    _repo.RemoveClaim(claims[0]);
                }
                else
                {
                    getNextClaim = false;
                }
            }
        }

        public void AddClaim()
        {
            Claims claim = new Claims();
            Console.WriteLine("Claim ID: ");
            claim.ClaimID = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the description of the claim: ");
            claim.Description = Console.ReadLine();

            Console.WriteLine("Claim amount: ");
            claim.ClaimAmount = int.Parse(Console.ReadLine());

            Console.WriteLine("Date of incident (yyyy/mm/dd):");
            claim.DateofIncident = DateTime.ParseExact(Console.ReadLine(), "yyyy/mm/dd", null);

            Console.WriteLine("Date of Filing: ");
            claim.DateOfClaim = DateTime.ParseExact(Console.ReadLine(), "yyyy/mm/dd", null);

            Console.WriteLine("What type of claim is this?\n");
            Console.WriteLine("1) Car");
            Console.WriteLine("2) Home");
            Console.WriteLine("3) Theft");

            string typeOfClaim = Console.ReadLine();
            switch (typeOfClaim)
            {
                case "1":
                    claim.TypeOfClaim = ClaimType.Car;
                    break;
                case "2":
                    claim.TypeOfClaim = ClaimType.Home;
                    break;
                case "3":
                    claim.TypeOfClaim = ClaimType.Theft;
                    break;
                default:
                    Console.WriteLine("Invalid choice selected");
                    break;
            }

            _repo.AddClaim(claim);
            Console.WriteLine($"Claim was successfully added.");
            if (claim.IsValid == true)
            {
                Console.WriteLine("This claim is valid.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("This claim is invalid.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        public void SeedContent()
        {
            DateTime incidentDate1 = new DateTime(2020, 01, 01);
            DateTime claimDate1 = new DateTime(2020, 02, 14);
            Claims claim1 = new Claims(1, ClaimType.Car, "Hit Deer", 1000m, incidentDate1, claimDate1);

            DateTime incidentDate2 = new DateTime(2020, 01, 01);
            DateTime claimDate2 = new DateTime(2020, 02, 14);
            Claims claim2 = new Claims(2, ClaimType.Home, "Hail damage to roof", 2300m, incidentDate2, claimDate2);

            DateTime incidentDate3 = new DateTime(2020, 01, 01);
            DateTime claimDate3 = new DateTime(2020, 02, 14);
            Claims claim3 = new Claims(3, ClaimType.Theft, "Firearm Stolen", 2000m, incidentDate3, claimDate3);

            _repo.AddClaim(claim1);
            _repo.AddClaim(claim2);
            _repo.AddClaim(claim3);
        }
    }
}