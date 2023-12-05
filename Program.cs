using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Assessment_1_Dungeon_Crawler_Character_Management
{
   
    public class Character
    {
        public string Name { get; set; }
        public string Class { get; set; }
        public int Level { get; set; }
        public int HitPoints { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Intelligence { get; set; }
        public List<Skill> Skills { get; set; } = new List<Skill>();

        public Character(string characterName, string characterClass, int characterStrength, int characterDexterity, int characterIntelligence)
        {
            Name = characterName;
            Class = characterClass;
            Level = 1;
            Strength = characterStrength;
            Dexterity = characterDexterity;
            Intelligence = characterIntelligence;
            HitPoints = 10 + Strength;
        }
        public void CheckSkillRequirements()
        {
            Console.WriteLine("Available Skills:");

            if (Strength >= 10)
            {
                Skill godlikeStrength = new Skill("Godlike Strength", "Wield the strength of the Gods.", "Strength");
                Console.WriteLine($"- {godlikeStrength.Name}: Increases {godlikeStrength.Attribute}, {godlikeStrength.Description}");
                Skills.Add(godlikeStrength);
            }

            if (Dexterity >= 10)
            {
                Skill felineReflexes = new Skill("Feline Reflexes", "Possess the speed and grace of a cat.", "Dexterity");
                Console.WriteLine($"- {felineReflexes.Name}: Increases {felineReflexes.Attribute}, {felineReflexes.Description}");
                Skills.Add(felineReflexes);
            }

            if (Intelligence >= 10)
            {
                Skill geniusIntellect = new Skill("Walking Library", "Annoy everyone with all your facts.", "Intelligence");
                Console.WriteLine($"- {geniusIntellect.Name}: Increases {geniusIntellect.Attribute}, {geniusIntellect.Description}");
                Skills.Add(geniusIntellect);
            }
        }
        public void UpdateCharacterAttributes(Skill skill)
        {
            switch (skill.Attribute)
            {
                case "Strength":
                    Strength += 10;
                    break;
                case "Dexterity":
                    Dexterity += 10;
                    break;
                case "Intelligence":
                    Intelligence += 10;
                    break;
            }
        }
    }


    public class Skill
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Attribute { get; set; }
        public Skill(string name, string description, string attribute)
        {
            Name = name;
            Description = description;
            Attribute = attribute;
        }
    }
        internal class Program
        {
            static List<Character> characters = new List<Character>();
            static List<Skill> skills = new List<Skill>();
            

            static void Main(string[] args)
            {

            bool quit = false;
            while (!quit)
            {
                DisplayMenu();
                int option;
                if (int.TryParse(Console.ReadLine(), out option))
                {
                    
                    switch (option)
                    {
                        case 1:
                            CreateCharacter();
                            break;
                        case 2:
                            AssignSkills();
                            break;
                        case 3:
                            LevelUp();
                            break;
                        case 4:
                            CharacterSheet();
                            break;
                        case 5:
                            quit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid option. Please press enter and choose again.");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid option. Please enter a number.");
                    Console.ReadLine();
                    Console.Clear();
                }

            }
        }
        static void DisplayMenu()
        {
            Console.WriteLine("Welcome to Create-A-Character!");
            Console.WriteLine("Please select a number to proceed:");
            Console.WriteLine("1. Create a new character.");
            Console.WriteLine("2. Assign skills to this character.");
            Console.WriteLine("3. Level up this character.");
            Console.WriteLine("4. Display current character sheet.");
            Console.WriteLine("5. Quit:");
        }
        static void CreateCharacter()
            {
                Console.WriteLine("What is your character's name?");
                string characterName = Console.ReadLine();
                Console.WriteLine($"What is {characterName}'s class?");
                string characterClass = Console.ReadLine();
                Console.WriteLine($"What are {characterName}'s current Strength, Dexterity, and Intelligence score?");
                Console.WriteLine($"{characterName}'s strength:");
                int characterStrength;
                while (!int.TryParse(Console.ReadLine(), out characterStrength))
                {
                    Console.WriteLine("Please enter a number.");
                }
                Console.WriteLine($"{characterName}'s dexterity:");
                int characterDexterity;
                while (!int.TryParse(Console.ReadLine(), out characterDexterity))
                {
                    Console.WriteLine("Please enter a number.");
                }
                Console.WriteLine($"{characterName}'s intelligence:");
                int characterIntelligence;
                while (!int.TryParse(Console.ReadLine(), out characterIntelligence))
                {
                    Console.WriteLine("Please enter a number.");
                }

            Character newCharacter = new Character(characterName, characterClass, characterStrength, characterDexterity, characterIntelligence);
            characters.Add(newCharacter);

            Console.WriteLine($"Character {characterName} created! Press enter to continue.");
            Console.ReadLine();
        
            Console.Clear();
            
        }
            static void AssignSkills()
        {
            DisplayCharacterList();
            Console.WriteLine("Please enter a character:");
            string characterName = Console.ReadLine();
            Character selectedCharacter = characters.Find(c => c.Name == characterName);

            if (selectedCharacter != null)
            {
                selectedCharacter.CheckSkillRequirements();

                Console.WriteLine($"Please enter which skill name you want to assign to {characterName}. You may only choose one.");

                string selectedSkillName = Console.ReadLine();

                Skill selectedSkill = selectedCharacter.Skills.Find(s => s.Name == selectedSkillName);

                if (selectedSkill != null)
                {
                    // Add the selected skill to the character's skill list
                    selectedCharacter.Skills.Add(selectedSkill);

                    Console.WriteLine($"{selectedSkillName} assigned to {characterName}! Press enter to continue.");
                    selectedCharacter.UpdateCharacterAttributes(selectedSkill);
                    Console.ReadLine();
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("Invalid skill selection.");
                }
            }
            else
            {
                Console.WriteLine("Character not found.");
            }
        }


        

            static void DisplayCharacterList()
            {
                Console.WriteLine("Saved Character List:");
                foreach (Character character in characters)
                {
                    Console.WriteLine($"- {character.Name}");
                }
            }
            static void LevelUp()
            {
                DisplayCharacterList();
                Console.Write("Enter the name of the character to level up: ");
                string characterName = Console.ReadLine();

                Character selectedCharacter = characters.Find(c => c.Name == characterName);

                if (selectedCharacter != null)
                {
                    selectedCharacter.Level++;
                    selectedCharacter.HitPoints += 5;

                    Console.WriteLine($"{characterName} leveled up!");
                    Console.WriteLine($"Current attributes: Strength: {selectedCharacter.Strength}, Dexterity: {selectedCharacter.Dexterity}, Intelligence: {selectedCharacter.Intelligence}");

                    Console.Write("Distribute 3 additional attribute points. \nEnter points for Strength: ");
                    selectedCharacter.Strength += int.Parse(Console.ReadLine());

                    Console.Write("Enter points for Dexterity: ");
                    selectedCharacter.Dexterity += int.Parse(Console.ReadLine());

                    Console.Write("Enter points for Intelligence: ");
                    selectedCharacter.Intelligence += int.Parse(Console.ReadLine());

                    Console.WriteLine($"{characterName}'s attributes updated! Press enter to continue.");
                Console.ReadLine();
                Console.Clear();

            }
                else
                {
                    Console.WriteLine("Character not found.");
                }
          
        }




            static void CharacterSheet()
            {
                DisplayCharacterList();
                Console.Write("Enter the name of the character to display the character sheet: ");
                string characterName = Console.ReadLine();

                Character selectedCharacter = characters.Find(c => c.Name == characterName);

                if (selectedCharacter != null)
                {
                    Console.WriteLine($"Character Sheet for {characterName}:");
                    Console.WriteLine($"Class: {selectedCharacter.Class}");
                    Console.WriteLine($"Level: {selectedCharacter.Level}");
                    Console.WriteLine($"Hit Points: {selectedCharacter.HitPoints}");
                    Console.WriteLine($"Attributes: Strength: {selectedCharacter.Strength}, Dexterity: {selectedCharacter.Dexterity}, Intelligence: {selectedCharacter.Intelligence}");
                    Console.WriteLine($"Skills:");
                        if (selectedCharacter.Skills.Count > 0)
                        {
                            Skill selectedSkill = selectedCharacter.Skills.Last();

                            Console.WriteLine($"{selectedSkill.Name}, {selectedSkill.Attribute}, {selectedSkill.Description}");
                        }
                        else
                        {
                            Console.WriteLine("No skills assigned yet.");
                        }
                Console.WriteLine("Press Enter to continue.");
                Console.ReadLine();
                Console.Clear();
            }
                else
                {
                    Console.WriteLine("Character not found.");
                }
            }
        }
}

