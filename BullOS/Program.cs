using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading;


namespace BullOS
{
    class Program
    {
        static List<string> usernames = new List<string>();
        static List<string> passwords = new List<string>();
        static List<string> word = new List<string>();
        static List<string> commands = new List<string>() { "!help", "!password", "!word", "!users", "!deleteaccount", "!logout", };
        static void Main(string[] args)
        {
            int user = 0;
            bool active = true;
            bool loggedIn = false;
            while (active)
            {
                //choose to login or create new account
                if (!usernames.Any()) createAccount();
                else
                {
                    Console.WriteLine("Do you want to login on an existing account or create a new one?");
                    Console.Write("Type \"!login\" or \"!new\" to create a new account: ");
                    string response = Console.ReadLine().ToLower();
                    while (!(response.Contains("!login")) && !(response.Contains("!new")))
                    {
                        Console.Write("Type \"!login\" or \"!new\" to create a new account: ");
                        response = Console.ReadLine().ToLower();
                    }
                    if (response.Contains("!login")) loginAccount();
                    else if (response.Contains("!new")) createAccount();
                }
            }

            static string UppercaseFirst(string s)
            {
                // Check for empty string.
                if (string.IsNullOrEmpty(s))
                {
                    return string.Empty;
                }
                // Return char and concat substring.
                return char.ToUpper(s[0]) + s.Substring(1);
            }

            void createAccount()
            {
                //create username
                Console.WriteLine("Create a personal account \n");
                Console.Write("First choose a username: ");
                string tempName = Console.ReadLine().ToLower();
                while (tempName == "" || tempName.Contains(" ") || usernames.Contains(tempName) || tempName.StartsWith("!"))
                {
                    if (usernames.Contains(tempName)) Console.WriteLine("That name is already taken");
                    else Console.WriteLine("Forbidden character or left blank");
                    Console.Write("Choose another username: ");
                    tempName = Console.ReadLine().ToLower();
                }
                usernames.Add(tempName);

                //create password
                Console.Write("\nChoose a password: ");
                string tempPass = Console.ReadLine();
                while (tempPass == "" || tempPass.Contains(" ") || tempPass.StartsWith("!"))
                {
                    Console.WriteLine("\nInvalid character used");
                    Console.Write("Please enter a valid password: ");
                    tempPass = Console.ReadLine();
                }
                passwords.Add(tempPass);
                word.Add("");
                //Console.Clear();
            }

            void loginAccount()
            {
                //check for username
                //Console.Clear();
                Console.Write("Enter your username: ");
                string username = Console.ReadLine().ToLower();
                while (!usernames.Contains(username))
                {
                    if (username.ToLower().Contains("!quit")) break;
                    //Console.Clear();
                    Console.WriteLine($"The account {username} does not exist");
                    Console.Write("Enter an existing username or type \"!quit\" to exit: ");
                    username = Console.ReadLine().ToLower();
                }

                if (!(username.ToLower().Contains("!quit")))
                {
                    //check for password
                    Console.Write("\nEnter your password: ");
                    string password = Console.ReadLine();
                    while (usernames.IndexOf(username) != passwords.IndexOf(password) && !(password.ToLower().Contains("!quit")))
                    {
                        //Console.Clear();
                        Console.WriteLine("You entered a wrong password\n");
                        Console.Write("Enter another password or type \"!quit\" to exit: ");
                        password = Console.ReadLine();
                        if (usernames.IndexOf(username) == passwords.IndexOf(password)) break;
                    }
                    if (usernames.IndexOf(username) == passwords.IndexOf(password))
                    {
                        user = usernames.IndexOf(username);
                        //Console.Clear();
                        Console.WriteLine($"Welcome {UppercaseFirst(username)}");
                        loggedIn = true;
                        while (loggedIn) account();
                    }
                }
            }

            void account()
            {
                Console.Write("\nType in a command or \"!help\" for a list of commands: ");
                string command = Console.ReadLine().ToLower();
                switch (command)
                {
                    case "!logout":
                        loggedIn = false;
                        break;
                    case "!help":
                        Console.Write("Commands available: ");
                        foreach (string i in commands) Console.Write(i + ", ");
                        Console.WriteLine();
                        break;
                    case "!word":
                        Console.Write("Existing text: ");
                        Console.WriteLine(word[user]);
                        Console.WriteLine("\nType \"!edit\" to edit text or press \"enter\" to leave");
                        string tempWord;
                        if (Console.ReadLine().ToLower().Contains("!edit"))
                        {
                            Console.Write("Write here: ");
                            tempWord = Console.ReadLine();
                            word.RemoveAt(user);
                            word.Insert(user, tempWord);
                        }
                        break;
                    case "!users":
                        foreach (string i in usernames) Console.Write($"{i}, ");
                        Console.WriteLine("");
                        break;
                    case "!deleteaccount":
                        usernames.RemoveAt(user);
                        passwords.RemoveAt(user);
                        word.RemoveAt(user);
                        loggedIn = false;
                        break;
                    case "!password":
                        Console.Write("Choose a new password: ");
                        string tempPass = Console.ReadLine();
                        while (tempPass == "" || tempPass.Contains(" "))
                        {
                            if (tempPass == "") Console.WriteLine("Can't leave blank");
                            else Console.WriteLine("Can't include spaces");
                            Console.Write("Please enter a valid password: ");
                            tempPass = Console.ReadLine();
                        }
                        passwords.RemoveAt(user);
                        passwords.Insert(user, tempPass);
                        Console.WriteLine("Password succesfully changed!");
                        break;
                    case "!delacc":
                        Console.WriteLine("Choose what account to delete");
                        break;
                    default:
                        if (command.StartsWith("!")) Console.WriteLine($"The command \"{command}\" doesn't exist");
                        else Console.WriteLine("Please begin your commands with a \"!\"");
                        break;
                }
            }
        }
    }
}