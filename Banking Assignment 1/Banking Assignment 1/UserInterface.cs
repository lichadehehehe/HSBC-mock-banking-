//input email credentials on line 391 and line 1144 to enable the email sending function




using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Banking_Assignment_1
{

    class UserInterface
    {
        //the boolin value to check the login status, default is false
        bool loginStatus = false;


        static void Main(string[] args)
        {
            //initialzie the login interface
            UserInterface ConsoleInterface = new UserInterface();
            //while the login status is false, show the interface, else, dismiss
            do
            {
                if (ConsoleInterface.loginStatus == false)
                    ConsoleInterface.LoginScreen(10, 40, 2, 10);


            } while (ConsoleInterface.loginStatus == true);



        }

        public static System.Drawing.Point Position { get; set; }
        protected static int origRow;
        protected static int origCol;
        string[] loginWindowFields = { "Username: ", "Password: " };


        int[,] loginFieldsPos = new int[2, 2];
        string[] loginUserInputs = new string[2];

        public void LoginScreen(int noLines, int formWidth, int startRow, int startCol)
        {

            Console.Clear();
            origRow = Console.CursorTop;
            origCol = Console.CursorLeft;
            


            for (int line = 0; line < noLines; line++)
            {

                if (line == 0 | line == 2 | line == (noLines - 1))
                {
                    for (int col = 0; col < formWidth; col++)
                    {
                        WriteAt("=", startCol + col, startRow + line);
                    }

                }
                else
                {
                    WriteAt("|", startCol, startRow + line);
                    WriteAt("|", startCol + formWidth - 1, startRow + line);
                }
            }

            WriteAt("    HSBC-UTS BANKING SYSTEM", startCol + 5, startRow + 1);
            WriteAt("Please Login to Start", startCol + 10, startRow + 4);


            int item = 0;
            foreach (string fieldName in loginWindowFields)
            {
                WriteAt(fieldName, startCol + 6, startRow + 6 + item);
                loginFieldsPos[item, 1] = Console.CursorTop;
                loginFieldsPos[item, 0] = Console.CursorLeft;

                int LoginCursorX = Console.CursorTop;
                int LoginCursorY = Console.CursorLeft;

                item++;

            }




            if (loginStatus == false)
            {

                for (int field = 0; field < item; field++)
                {

                    Console.SetCursorPosition(loginFieldsPos[field, 0], loginFieldsPos[field, 1]);

                    if (field == 1)
                    {

                        var pass = string.Empty;
                        ConsoleKey key;
                        do
                        {
                            var keyInfo = Console.ReadKey(intercept: true);
                            key = keyInfo.Key;

                            if (key == ConsoleKey.Backspace && pass.Length > 0)
                            {
                                Console.Write("\b \b");
                                pass = pass[0..^1];
                            }
                            else if (!char.IsControl(keyInfo.KeyChar))
                            {
                                Console.Write("*");
                                pass += keyInfo.KeyChar;
                            }
                        } while (key != ConsoleKey.Enter);
                        loginUserInputs[field] = pass;

                    }

                    else
                    {

                        loginUserInputs[field] = Console.ReadLine();

                    }

                }

                //read strings from login.txt for login credentials
                string[] content = File.ReadAllLines("login.txt");

                int arraySize = content.Length;



                for (int i = 0; i < arraySize; i++)

                {

                    string[] splits = content[i].Split('|');
                    
                    //if the username and password is a match, display main menu
                    if (loginUserInputs[0] == splits[0] && loginUserInputs[1] == splits[1])
                    {

                        WriteAt("Welcome! Press any key to continue.", startCol, noLines + 2);

                        Console.ReadKey();
                        MainInterface ConsoleInterface1 = new MainInterface();
                        ConsoleInterface1.MainScreen();
                        loginStatus = true;
                        break;

                    }


                }


                //if login failed, redisplay the login screen
                if (loginStatus == false)
                {

                    WriteAt("Invalid credentials. Press any key to retry.", startCol, noLines + 2);
                    Console.ReadKey();

                    UserInterface ConsoleInterface = new UserInterface();

                    do
                    {
                        if (ConsoleInterface.loginStatus == false)
                            ConsoleInterface.LoginScreen(10, 40, 2, 10);
                        //this loop is essential otherwise the login page will keep on looping

                    } while (ConsoleInterface.loginStatus == true);

                }


            }



        }

        // thte write at method as demonstrated by the lecture example
        protected void WriteAt(string s, int col, int row)
        {

            try
            {
                Console.SetCursorPosition(origCol + col, origRow + row);
                Console.Write(s);
            }

            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }

        }


    }

    class MainInterface
    {

        public void MainScreen()
        {
            //clear console first
            Console.Clear();
            
            //print each line of the main menu
            Console.WriteLine("╔═══════════════════════════════════════════════╗");
            Console.WriteLine("║\tHSBC-UTS BANKING SYSTEM\t                ║");
            Console.WriteLine("║═══════════════════════════════════════════════║");
            Console.WriteLine("║ 1. Create a new account\t\t\t║");
            Console.WriteLine("║ 2. Search for an account\t\t\t║");
            Console.WriteLine("║ 3. Deposit                                    ║");
            Console.WriteLine("║ 4. Withdraw                                   ║");
            Console.WriteLine("║ 5. A/C Statement                              ║");
            Console.WriteLine("║ 6. Delete Account                             ║");
            Console.WriteLine("║ 7. Exit                                       ║");
            Console.WriteLine("╚═══════════════════════════════════════════════╝");
            Console.WriteLine("║ \t\t\t\t\t\t║");
            Console.WriteLine("║ Enter your Choice (1-7):\t\t\t║");
            Console.WriteLine("╚═══════════════════════════════════════════════╝");

            string menuSelection = Console.ReadLine();
            //detect user input into different option of the menu
            switch (menuSelection)
            {

                case "1":
                    //case 1 display create account interface
                    MainInterface ConsoleInterface1 = new MainInterface();
                    Console.Clear();
                    ConsoleInterface1.createAccountScreen();

                    break;



                case "2":
                    //case 2 display search account interface
                    MainInterface ConsoleInterface2 = new MainInterface();
                    ConsoleInterface2.searchAccountScreen();


                    break;

                case "3":
                    //case 3 display deposit interface
                    MainInterface ConsoleInterface3 = new MainInterface();
                    Console.Clear();
                    ConsoleInterface3.depositScreen();


                    break;

                case "4":
                    //case 4 display withdraw interface
                    MainInterface ConsoleInterface4 = new MainInterface();
                    Console.Clear();
                    ConsoleInterface4.withdrawlScreen();


                    break;

                case "5":
                    //case 5 display statement interface
                    MainInterface ConsoleInterface5 = new MainInterface();
                    Console.Clear();
                    ConsoleInterface5.statementScreen();


                    break;

                case "6":
                    //case 6 display delte interface
                    MainInterface ConsoleInterface6 = new MainInterface();
                    Console.Clear();
                    ConsoleInterface6.deleteScreen();


                    break;

                case "7":
                    //case 7 exit the program with environment.exit(0)
                    Environment.Exit(0);


                    break;

                default:
                    //any other choice is invalid, will redisplay the main menu
                    Console.WriteLine("Invalid choice, press any key to try again.");
                    Console.ReadKey();
                    MainInterface ConsoleInterface8 = new MainInterface();
                    ConsoleInterface8.MainScreen();
                    break;

            }


        }



        private void createAccountScreen()
        {   
            //interger account number and the double account balance
            int accountNumber;
            double accountBalance;
            //print each line of the creat account screen
            Console.WriteLine("╔═══════════════════════════════════════════════╗");
            Console.WriteLine("║              CREATE A NEW ACCOUNT             ║");
            Console.WriteLine("║═══════════════════════════════════════════════║");
            Console.WriteLine("║              ENTER THE DETAILS                ║");
            Console.WriteLine("║ 1. First Name: \t\t\t\t║");
            Console.WriteLine("║ 2. Last Name: \t\t\t\t║");
            Console.WriteLine("║ 3. Address:                                   ║");
            Console.WriteLine("║ 4. Phone:                                     ║");
            Console.WriteLine("║ 5. Email:                                     ║");
            Console.WriteLine("╚═══════════════════════════════════════════════╝");

            //set cursorposition into each field and read the input
            Console.SetCursorPosition(16, 4);
            string firstName = Console.ReadLine();

            Console.SetCursorPosition(15, 5);
            string lastName = Console.ReadLine();

            Console.SetCursorPosition(13, 6);
            string address = Console.ReadLine();

            Console.SetCursorPosition(11, 7);
            string phone = Console.ReadLine();

            Console.SetCursorPosition(11, 8);
            string email = Console.ReadLine();

            //to verify email address, regex pattern are used
            //https://www.rhyous.com/2010/06/15/csharp-email-regular-expression/

            //if email address is valid and phone length is no more than 10 degits and phone number is valid interger
            //and first and last name and address is not null, display information
            if (new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$")
                .Match(email).Success &&
                phone.Length <= 10 &&
                Int32.TryParse(phone, out int phoneCheck) &&
                firstName != "" &&
                lastName != "" &&
                address != "")
            {

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Please verify the information");
                Console.WriteLine("First Name: " + firstName);
                Console.WriteLine("Last Name: " + lastName);
                Console.WriteLine("Address: " + address);
                Console.WriteLine("Phone: " + phone);
                Console.WriteLine("Email: " + email);
                Console.WriteLine("Confirm input?");
                Console.WriteLine("(Press Y to conform, press any other key to decline)");

                switch (Console.ReadLine())
                {
                    //case y, confirm information and send the account information via email
                    case "y":
                    case "Y":
                        {   //randomly generate an integer for the account number
                            accountNumber = new Random().Next(00000000, 99999999);
                            //initial account balance 0
                            accountBalance = 0;
                            //method to send email, via the gmail server
                            //in this case, we use the gmail client as an example
                            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                            //input the email credentials, first the email address, then the email password
                            client.Credentials = new NetworkCredential("INSERT YOUR EMAIL ADDRESS", "INSERT YOUR PASSWORD");
                            //enable ssl 
                            client.EnableSsl = true;
                            bool emailSentStatus = true;
                            try
                            {
                                //send the account information to the nominated email adress
                                MailMessage emailContent = new MailMessage(new MailAddress("do-not-reply@hsbc-uts.com", "HSBC-UTS Banking"),
                                                      new MailAddress(email, firstName));
                                emailContent.Subject = firstName + ", Welcome to HSBC-UTS Banking";
                                //use html for email format
                                emailContent.IsBodyHtml = true;
                                emailContent.Body = string.Format(
                              $"Dear {firstName},<br><br>" +
                              "Thank you for choosing HSBC-UTS Banking.<br><br>" +
                              "Your account details are as follows:<br>" +

                              $"Account number: {accountNumber}<br>" +
                              $"First name: {firstName}<br>" +
                              $"Last name: {lastName}<br>" +
                              $"Address: {address}<br>" +
                              $"Phone: {phone}<br>" +
                              $"Email: {email}<br><br>" +

                              "Regards,<br>" +
                              "HSBC-UTS Banking<br><br>" +
                              "Please do not reply to this email as this mail box is unattended");
                                //send the email and change the email status to true
                                client.Send(emailContent);
                                emailSentStatus = true;

                            }
                            catch
                            {

                                emailSentStatus = false;

                            }

                            if (emailSentStatus == true)
                            {

                                Console.WriteLine("Account successfully created.");
                                Console.WriteLine("Please check the nominated email address for account information.");
                                Console.ReadKey();
                            }

                            else
                            {
                                //if email can not be send display the error message
                                Console.WriteLine("Account successfully created.");
                                Console.WriteLine("Error: account information can not be sent via email.");
                                Console.WriteLine("This could be a result of a Gmail security restriction. " +
                                                  "Please contact me via 12426894@student.uts.edu.au");
                                Console.ReadKey();

                            }
                            //display the generated account number
                            Console.WriteLine("Account number for " + firstName + "" + lastName + " is: " + accountNumber);

                            //write the account information into the accountnumber.txt file
                            using (StreamWriter accountDetail = File.CreateText(Path.Combine(accountNumber.ToString()) + ".txt"))
                            {
                                accountDetail.WriteLine("First Name|" + firstName);
                                accountDetail.WriteLine("Last Name|" + lastName);
                                accountDetail.WriteLine("Address|" + address);
                                accountDetail.WriteLine("Phone|" + phone);
                                accountDetail.WriteLine("Email|" + email);
                                accountDetail.WriteLine("AccountNo|" + accountNumber);
                                accountDetail.WriteLine("Balance|" + accountBalance);

                            }
                            //readkey() and go back to main interface
                            Console.ReadKey();
                            MainInterface ConsoleInterface2 = new MainInterface();
                            ConsoleInterface2.MainScreen();
                            break;
                        }

                    default:
                        {
                            //default case, user dismiss the new account setup process and go back to the createaccount screen
                            MainInterface ConsoleInterface1 = new MainInterface();
                            Console.Clear();
                            ConsoleInterface1.createAccountScreen();
                            break;

                        }

                }

            }


            else

            {   //if the account information is invalid, display the following information
                Console.WriteLine();
                Console.WriteLine("Invalid account detail.");
                Console.WriteLine("(Press Y to try again, press any other key to go back to main menu.)");

                switch (Console.ReadLine())
                {   //case y to go back to create account screen
                    case "y":
                    case "Y":
                        {

                            MainInterface ConsoleInterface1 = new MainInterface();
                            Console.Clear();
                            ConsoleInterface1.createAccountScreen();
                            break;

                        }
                        // case default to go back to main screen
                    default:
                        {
                            MainInterface ConsoleInterface1 = new MainInterface();
                            Console.Clear();
                            ConsoleInterface1.MainScreen();
                            break;
                        }

                }



            }


        }

        private void searchAccountScreen()
        {   //clear console and display the search account screen content
            Console.Clear();
            Console.WriteLine("╔═══════════════════════════════════════════════╗");
            Console.WriteLine("║               SEARCH AN ACCOUNT               ║");
            Console.WriteLine("║═══════════════════════════════════════════════║");
            Console.WriteLine("║               ENTER THE DETAILS               ║");
            Console.WriteLine("║ \tAccount Number:\t\t\t\t║");
            Console.WriteLine("╚═══════════════════════════════════════════════╝");
            //set the cursor position and read the input as the serached account number
            Console.SetCursorPosition(23, 4);
            string searchAccountNumber = Console.ReadLine();
            Console.SetCursorPosition(0, 7);

            //if the account file is found, display the accoutn information 
            if (File.Exists(Path.Combine(searchAccountNumber) + ".txt"))
            {

                string[] accountFile = File.ReadAllLines(Path.Combine(searchAccountNumber) + ".txt");
                //take the first 7 items from the account file array as only these are the account informaiton,
                //the rest are the transaction statement
                string[] accountInformation = accountFile.Take(7).ToArray();

                for (int i = 0; i < 7; i++)
                {   //display the account information, replace the | with :
                    Console.WriteLine(accountInformation[i].Replace('|', ':'));

                }

                Console.WriteLine();
                Console.WriteLine("(Press any key to continue searching, press N to go back to main menu.)");
                switch (Console.ReadLine())
                {   // case n go back to main menu, case default will continue on the search screen
                    case "N":
                    case "n":
                        {

                            MainInterface ConsoleInterface1 = new MainInterface();
                            Console.Clear();
                            ConsoleInterface1.MainScreen();
                            break;



                        }

                    default:
                        {
                            MainInterface ConsoleInterface1 = new MainInterface();
                            Console.Clear();
                            ConsoleInterface1.searchAccountScreen();
                            break;

                        }

                }

            }

            else
            {   //if account number not found, display the following message
                Console.WriteLine("Account not found.");
                Console.WriteLine("(Press any key to continue searching, press N to go back to main menu.)");
                switch (Console.ReadLine())
                {   //case n go back to main screen, case default would continue on the search screen
                    case "N":
                    case "n":
                        {

                            MainInterface ConsoleInterface1 = new MainInterface();
                            Console.Clear();
                            ConsoleInterface1.MainScreen();
                            break;

                        }

                    default:
                        {
                            MainInterface ConsoleInterface1 = new MainInterface();
                            Console.Clear();
                            ConsoleInterface1.searchAccountScreen();
                            break;

                        }

                }

            }


        }



        private void depositScreen()
        {   //diaply the deposit screen interface
            Console.WriteLine("╔═══════════════════════════════════════════════╗");
            Console.WriteLine("║                   DEPOSIT                     ║");
            Console.WriteLine("║═══════════════════════════════════════════════║");
            Console.WriteLine("║              ENTER THE DETAILS                ║");
            Console.WriteLine("║                                               ║");
            Console.WriteLine("║ \tAccount Number:\t\t\t\t║");
            Console.WriteLine("║ \tAmount: $\t\t\t\t║");
            Console.WriteLine("╚═══════════════════════════════════════════════╝");
            //set the surcor position and read the input as the searched account number
            Console.SetCursorPosition(23, 5);
            string searchAccountNumber = Console.ReadLine();

            //if the account file exists, display the following information
            if (File.Exists(Path.Combine(searchAccountNumber) + ".txt"))
            {
                Console.SetCursorPosition(0, 8);
                //read all information from the acocunt file
                string[] accountFile = File.ReadAllLines(Path.Combine(searchAccountNumber) + ".txt");
                //take the first 7 items in the account file array as these are the account information
                string[] accountInformation = accountFile.Take(7).ToArray();
                int arraySize = accountFile.Length;
                int displayArraySize = accountInformation.Length;
                List<string> accountInfoList = new List<string>();


                for (int i = 0; i < 7; i++)
                {   //display the account information from the array, replace each | with :
                    Console.WriteLine(accountInformation[i].Replace('|', ':'));

                }

                // display the account found message, set cursor position to read user input of deposit amount
                Console.WriteLine();
                Console.WriteLine("Account found. Please enter the deposit amount.");
                Console.SetCursorPosition(17, 6);
                string depositAmount = Console.ReadLine();

                //if the user input a valid double amount
                if (Double.TryParse(depositAmount, out double checkDepositAmount))
                {

                    foreach (string line in accountFile)
                    {
                        //read all lines from the account.txt file into the accountinfolist arraylist
                        string[] splitInfo = line.Split('|');
                        string parsedInfo = splitInfo[1].ToString();
                        accountInfoList.Add(parsedInfo);

                    }

                    //add the deposit amount on the basis of the balance, get the new balance
                    double newBalance = Convert.ToDouble(accountInfoList[6]) + Convert.ToDouble(depositAmount);


                    for (int i = 0; i < arraySize; i++)
                    {
                        if (i == 0)
                        {   //from the first string of the array list, initiate the writealltext to overwrite the file
                            File.WriteAllText(Path.Combine(searchAccountNumber) + ".txt", accountFile[i] + "\n");


                        }



                        else if (i != 6)
                        {   
                            //if the item number is not 6, which is not on the balance row, append the document
                            File.AppendAllText(Path.Combine(searchAccountNumber) + ".txt", accountFile[i] + "\n");

                        }

                        else
                        {
                            //if the item number is at 6, which is the balance row, append the new balance accordingly
                            File.AppendAllText(Path.Combine(searchAccountNumber) + ".txt", "Balance|" + newBalance + "\n");

                        }



                    }

                    //append the transaction statement to the end of the file
                    File.AppendAllText(Path.Combine(searchAccountNumber) + ".txt",
                        DateTime.Today.ToString("d").Replace('/', '.') + "|Deposit|" + depositAmount + "|" + newBalance);

                    Console.SetCursorPosition(0, 8 + displayArraySize + 4);
                    Console.WriteLine("Deposit successfull.");
                    Console.WriteLine("Continue deposit?");
                    Console.WriteLine("(Press Y to continue deposit, press any other key to go back to main menu.)");
                    switch (Console.ReadLine())
                    {   //case y continue on the deposit screen, case default go back to main menu
                        case "Y":
                        case "y":
                            {

                                MainInterface ConsoleInterface1 = new MainInterface();
                                Console.Clear();
                                ConsoleInterface1.depositScreen();
                                break;



                            }

                        default:
                            {
                                MainInterface ConsoleInterface1 = new MainInterface();
                                Console.Clear();
                                ConsoleInterface1.MainScreen();
                                break;

                            }

                    }



                }

                //if user input an invalid deposit ammount, display the following message
                else
                {
                    Console.SetCursorPosition(0, 8 + arraySize + 4);
                    Console.WriteLine("Invalid deposit amount.");
                    Console.WriteLine("(Press Y to continue deposit, press any other key to go back to main menu.)");
                    switch (Console.ReadLine())
                    {   //case y remain oin the deposit screen, default case go back to main menu
                        case "Y":
                        case "y":
                            {

                                MainInterface ConsoleInterface1 = new MainInterface();
                                Console.Clear();
                                ConsoleInterface1.depositScreen();
                                break;



                            }

                        default:
                            {
                                MainInterface ConsoleInterface1 = new MainInterface();
                                Console.Clear();
                                ConsoleInterface1.MainScreen();
                                break;

                            }

                    }


                }


            }

            // if the account is not found, display the following message
            else
            {
                Console.SetCursorPosition(0, 8);
                Console.WriteLine("Account not found.");
                Console.WriteLine("(Press Y to continue deposit, press any other key to go back to main menu.)");
                switch (Console.ReadLine())
                {   //case y remain on the deposit screen, default case go back to main menu 
                    case "Y":
                    case "y":
                        {

                            MainInterface ConsoleInterface1 = new MainInterface();
                            Console.Clear();
                            ConsoleInterface1.depositScreen();
                            break;

                        }

                    default:
                        {
                            MainInterface ConsoleInterface1 = new MainInterface();
                            Console.Clear();
                            ConsoleInterface1.MainScreen();
                            break;

                        }

                }

            }


        }



        private void withdrawlScreen()
        {   //printout the withdrawlscreen interface
            Console.WriteLine("╔═══════════════════════════════════════════════╗");
            Console.WriteLine("║                   WITHDRAW                    ║");
            Console.WriteLine("║═══════════════════════════════════════════════║");
            Console.WriteLine("║              ENTER THE DETAILS                ║");
            Console.WriteLine("║                                               ║");
            Console.WriteLine("║ \tAccount Number:\t\t\t\t║");
            Console.WriteLine("║ \tAmount: $\t\t\t\t║");
            Console.WriteLine("╚═══════════════════════════════════════════════╝");
            //set cursor position and read the user input as the searched account number
            Console.SetCursorPosition(23, 5);
            string searchAccountNumber = Console.ReadLine();
            //if the account file exists, display the following information
            if (File.Exists(Path.Combine(searchAccountNumber) + ".txt"))
            {
                Console.SetCursorPosition(0, 8);
                //same as the deposit process, read information from the account file
                string[] accountFile = File.ReadAllLines(Path.Combine(searchAccountNumber) + ".txt");
                string[] accountInformation = accountFile.Take(7).ToArray();
                int arraySize = accountFile.Length;
                int displayArraySize = accountInformation.Length;
                List<string> accountInfoList = new List<string>();


                for (int i = 0; i < 7; i++)
                {
                    Console.WriteLine(accountInformation[i].Replace('|', ':'));

                }


                Console.WriteLine();
                Console.WriteLine("Account found. Please enter the withdrawl amount.");
                Console.SetCursorPosition(17, 6);
                string depositAmount = Console.ReadLine();
                if (Double.TryParse(depositAmount, out double checkDepositAmount))
                {

                    foreach (string line in accountFile)
                    {

                        string[] splitInfo = line.Split('|');
                        string parsedInfo = splitInfo[1].ToString();
                        accountInfoList.Add(parsedInfo);

                    }


                    double newBalance = Convert.ToDouble(accountInfoList[6]) - Convert.ToDouble(depositAmount);
                    //check the balance restriction, if the balance is over 0, approve the withdrawl
                    if (newBalance >= 0)
                    {

                        for (int i = 0; i < arraySize; i++)
                        {
                            if (i == 0)
                            {
                                File.WriteAllText(Path.Combine(searchAccountNumber) + ".txt", accountFile[i] + "\n");


                            }



                            else if (i != 6)
                            {
                                File.AppendAllText(Path.Combine(searchAccountNumber) + ".txt", accountFile[i] + "\n");

                            }

                            else
                            {

                                File.AppendAllText(Path.Combine(searchAccountNumber) + ".txt", "Balance|" + newBalance + "\n");

                            }



                        }

                        File.AppendAllText(Path.Combine(searchAccountNumber) + ".txt",
                        DateTime.Today.ToString("d").Replace('/', '.') + "|Withdraw|" + depositAmount + "|" + newBalance);

                        Console.SetCursorPosition(0, 8 + displayArraySize + 4);
                        Console.WriteLine("Withdrawl successfull.");
                        Console.WriteLine("Continue withdrawl?");
                        Console.WriteLine("(Press Y to continue withdrawl, press any other key to go back to main menu.)");
                        switch (Console.ReadLine())
                        {
                            case "Y":
                            case "y":
                                {

                                    MainInterface ConsoleInterface1 = new MainInterface();
                                    Console.Clear();
                                    ConsoleInterface1.withdrawlScreen();
                                    break;



                                }

                            default:
                                {
                                    MainInterface ConsoleInterface1 = new MainInterface();
                                    Console.Clear();
                                    ConsoleInterface1.MainScreen();
                                    break;

                                }

                        }



                    }
                    //else, if the balance is under 0, decline the withdrawl
                    else
                    {
                        Console.SetCursorPosition(0, 8 + displayArraySize + 4);
                        Console.WriteLine("Error: withdrawl amount exceeds available balance.");
                        Console.WriteLine("(Press Y to continue withdrawl, press any other key to go back to main menu.)");
                        switch (Console.ReadLine())
                        {
                            case "Y":
                            case "y":
                                {

                                    MainInterface ConsoleInterface1 = new MainInterface();
                                    Console.Clear();
                                    ConsoleInterface1.withdrawlScreen();
                                    break;



                                }

                            default:
                                {
                                    MainInterface ConsoleInterface1 = new MainInterface();
                                    Console.Clear();
                                    ConsoleInterface1.MainScreen();
                                    break;

                                }

                        }

                    }

                }
                // if the withdrawl amount is invalid, display the following information
                else
                {
                    Console.SetCursorPosition(0, 8 + arraySize + 4);
                    Console.WriteLine("Invalid withdrawl amount.");
                    Console.WriteLine("(Press Y to continue withdrawl, press any other key to go back to main menu.)");
                    switch (Console.ReadLine())
                    {
                        case "Y":
                        case "y":
                            {

                                MainInterface ConsoleInterface1 = new MainInterface();
                                Console.Clear();
                                ConsoleInterface1.withdrawlScreen();
                                break;



                            }

                        default:
                            {
                                MainInterface ConsoleInterface1 = new MainInterface();
                                Console.Clear();
                                ConsoleInterface1.MainScreen();
                                break;

                            }

                    }


                }


            }
            //if the account is not found, display the following information
            else
            {
                Console.SetCursorPosition(0, 8);
                Console.WriteLine("Account not found.");
                Console.WriteLine("(Press Y to continue withdrawl, press any other key to go back to main menu.)");
                switch (Console.ReadLine())
                {
                    case "Y":
                    case "y":
                        {

                            MainInterface ConsoleInterface1 = new MainInterface();
                            Console.Clear();
                            ConsoleInterface1.withdrawlScreen();
                            break;

                        }

                    default:
                        {
                            MainInterface ConsoleInterface1 = new MainInterface();
                            Console.Clear();
                            ConsoleInterface1.MainScreen();
                            break;

                        }

                }


            }


        }


        private void statementScreen()
        {

            //print out the statement interface

            Console.WriteLine("╔═══════════════════════════════════════════════╗");
            Console.WriteLine("║                  STATEMENT                    ║");
            Console.WriteLine("║═══════════════════════════════════════════════║");
            Console.WriteLine("║              ENTER THE DETAILS                ║");
            Console.WriteLine("║═══════════════════════════════════════════════║");
            Console.WriteLine("║ \tAccount Number:\t\t\t\t║");
            Console.WriteLine("╚═══════════════════════════════════════════════╝");
            //set cursor position to read the user input as the searched account number
            Console.SetCursorPosition(23, 5);
            string searchAccountNumber = Console.ReadLine();
            //if the account file exists, display the following information
            if (File.Exists(Path.Combine(searchAccountNumber) + ".txt"))
            {

                //initiate an array list called the email list
                List<string> emailList = new List<string>();
                

                Console.SetCursorPosition(0, 8);
                //read all information in the account file
                string[] accountFile = File.ReadAllLines(Path.Combine(searchAccountNumber) + ".txt");
                //skip the first 7 rows, head staight into the account transaction statement rows
                string[] statements = accountFile.Skip(7).ToArray();
                //for each transaction statement rows, append them into the array list
                foreach (string line in statements)
                {
                    emailList.Add(line);
                }
                //read the last 5 statements and append them into the email list
                string[] lastStatements = emailList.Take(5).ToArray();
                //reverse the emaillist array list
                emailList.Reverse();

                if (emailList.Count() < 5)
                {   //if the email list length is under 5, display all transactions
                    lastStatements = emailList.Take(emailList.Count()).ToArray();
                }
                else if (emailList.Count() >= 5)
                {   // if the email list length is equal or over 5, display only the last 5 of the transactions
                    lastStatements = emailList.Take(5).ToArray();
                }

                Console.WriteLine();
                Console.WriteLine("Last five transactions:");


                foreach (string statement in lastStatements)
                {
                    //read information from each row of the statement, split them according to field
                    //field 0 is the time
                    //field 1 is the description
                    //field 2 is the deposit/withdrawl ammount
                    //field 3 is the current balance
                    string[] statementField = statement.Split('|', StringSplitOptions.RemoveEmptyEntries);
                    string statementString = String.Format("Date: " + statementField[0] + " " + statementField[1] + " Amount: " + statementField[2] + " Current Balance: " + statementField[3]);
                    //output the statement into the screen
                    Console.WriteLine(statementString);


                }


                Console.WriteLine("Email account statement?");
                Console.WriteLine("(Press Y to email account statement, press any other key to go back to main menu.)");
                switch (Console.ReadLine())
                {   //case y email the statement, dafault case go back to main menu
                    case "Y":
                    case "y":
                        {

                            List<string> accountInfoList = new List<string>();

                            foreach (string line in accountFile)
                            {

                                string[] splitInfo = line.Split('|');
                                string parsedInfo = splitInfo[1].ToString();
                                accountInfoList.Add(parsedInfo);

                            }

                            string fiveStatementHTMLRow = "";
                            foreach (string statement in lastStatements)
                            {
                                
                                string[] statementField = statement.Split('|', StringSplitOptions.RemoveEmptyEntries);
                                for (int i = 0; i < 1; i++)
                                {   //input each row from the statement into the html format
                                    //add in to the fiveStatementHTMLRow string as html output
                                    fiveStatementHTMLRow += string.Format($"<tr><td>{statementField[0]}</td>" +
                                                    $"<td>{statementField[1]}</td><td>{statementField[2]}</td>" +
                                                    $"<td>{statementField[3]}</td></tr>");
                                }
                            }


                            //initiate email sequence and credentials
                            //in this case, we use the gmail client as an example
                            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                            client.Credentials = new NetworkCredential("INSERT YOUR EMAIL ADDRESS", "INSERT YOUR PASSWORD");
                            client.EnableSsl = true;
                            bool emailSentStatus = true;
                            try
                            {

                                MailMessage emailContent = new MailMessage(new MailAddress("do-not-reply@hsbc-uts.com", "HSBC-UTS Banking"),
                                                      new MailAddress(accountInfoList[4], accountInfoList[0]));

                                emailContent.Subject = "Account statement for " + accountInfoList[0];
                                emailContent.IsBodyHtml = true;

                                emailContent.Body = string.Format(
                              $"Dear {accountInfoList[0]},<br><br>" +
                              "Your account statement are as follows:<br>" +

                              "<table>" +
                              "<tr><th>Date</th><th>Description</th><th>Amount</th>" +
                              "<th>Balance</th></tr>" +
                              fiveStatementHTMLRow +
                              "</table><br>" +


                                "Regards,<br>" +
                              "HSBC-UTS Banking<br><br>" +
                              "Please do not reply to this email as this mail box is unattended");

                                client.Send(emailContent);
                                Console.WriteLine("Statement successfully emailed.");
                                Console.WriteLine("(Press any key to continue searching, press N to go back to main menu.)");
                                switch (Console.ReadLine())
                                {   //case n go back to main menu, default case continue on the statement screen
                                    case "N":
                                    case "n":
                                        {

                                            MainInterface ConsoleInterface1 = new MainInterface();
                                            Console.Clear();
                                            ConsoleInterface1.MainScreen();
                                            break;



                                        }

                                    default:
                                        {
                                            MainInterface ConsoleInterface1 = new MainInterface();
                                            Console.Clear();
                                            ConsoleInterface1.statementScreen();
                                            break;

                                        }


                                }


                            }
                            // if the email sending sequence resulted in an error
                            catch (SmtpException)
                            {
                                Console.WriteLine("Error: account information can not be sent via email.");
                                Console.WriteLine("This could be a result of a Gmail security restriction. " +
                                                  "Please contact me via 12426894@student.uts.edu.au");
                                Console.ReadKey();
                            }

                            break;
                        }


                    default:
                        {

                            MainInterface ConsoleInterface1 = new MainInterface();
                            Console.Clear();
                            ConsoleInterface1.MainScreen();
                            break;


                        }



                }



            }
            //if the account file is not found
            else
            {
                Console.WriteLine();
                Console.WriteLine("Account not found.");
                Console.WriteLine("(Press any key to continue searching, press N to go back to main menu.)");
                switch (Console.ReadLine())
                {   // case n go back to main menu, default case remain on the statement screen
                    case "N":
                    case "n":
                        {

                            MainInterface ConsoleInterface1 = new MainInterface();
                            Console.Clear();
                            ConsoleInterface1.MainScreen();
                            break;



                        }

                    default:
                        {
                            MainInterface ConsoleInterface1 = new MainInterface();
                            Console.Clear();
                            ConsoleInterface1.statementScreen();
                            break;

                        }

                }

            }
        }

        private void deleteScreen()
        {   //display the delete interface
            Console.WriteLine("╔═══════════════════════════════════════════════╗");
            Console.WriteLine("║               DELETE AN ACCOUNT               ║");
            Console.WriteLine("║═══════════════════════════════════════════════║");
            Console.WriteLine("║               ENTER THE DETAILS               ║");
            Console.WriteLine("║═══════════════════════════════════════════════║");
            Console.WriteLine("║ \tAccount Number:\t\t\t\t║");
            Console.WriteLine("╚═══════════════════════════════════════════════╝");

            Console.SetCursorPosition(23, 5);

            string searchAccountNumber = Console.ReadLine();
            //if the account file exists, display the account information
            if (File.Exists(Path.Combine(searchAccountNumber) + ".txt"))
            {
                Console.SetCursorPosition(0, 8);
                string[] accountFile = File.ReadAllLines(Path.Combine(searchAccountNumber) + ".txt");
                string[] accountInformation = accountFile.Take(7).ToArray();
                int arraySize = accountFile.Length;
                int displayArraySize = accountInformation.Length;
                List<string> accountInfoList = new List<string>();


                for (int i = 0; i < 7; i++)
                {
                    Console.WriteLine(accountInformation[i].Replace('|', ':'));

                }


                Console.WriteLine();
                Console.WriteLine("Confirm delete account?");
                Console.WriteLine("(Press Y to confrim delete. Press any other key to dismiss.)");

                switch (Console.ReadLine())
                {   //case y delete the account, default case go back to main menu
                    case "Y":
                    case "y":
                        {
                            File.Delete(Path.Combine(searchAccountNumber) + ".txt");
                            Console.WriteLine("Account destroyed.");

                            Console.WriteLine("(Press Y to continue deleting. Press any other key to go back to main menu.)");

                            switch (Console.ReadLine())
                            {
                                case "Y":
                                case "y":
                                    {

                                        MainInterface ConsoleInterface1 = new MainInterface();
                                        Console.Clear();
                                        ConsoleInterface1.deleteScreen();
                                        break;


                                    }

                                default:
                                    {
                                        MainInterface ConsoleInterface1 = new MainInterface();
                                        Console.Clear();
                                        ConsoleInterface1.MainScreen();
                                        break;

                                    }


                            }

                            break;

                        }

                    default:
                        {
                            MainInterface ConsoleInterface1 = new MainInterface();
                            Console.Clear();
                            ConsoleInterface1.MainScreen();
                            break;


                        }


                }
            }
            //if the acocunt is not found
            else
            {

                Console.WriteLine();
                Console.WriteLine("Account not found.");
                Console.WriteLine("(Press any key to continue searching, press N to go back to main menu.)");
                switch (Console.ReadLine())
                {   //case n go back to main menu, default case ramain on the delte interface
                    case "N":
                    case "n":
                        {

                            MainInterface ConsoleInterface1 = new MainInterface();
                            Console.Clear();
                            ConsoleInterface1.MainScreen();
                            break;



                        }

                    default:
                        {
                            MainInterface ConsoleInterface1 = new MainInterface();
                            Console.Clear();
                            ConsoleInterface1.deleteScreen();
                            break;

                        }

                }


            }
        }
    }
}
        
