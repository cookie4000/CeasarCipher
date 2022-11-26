using System.Text;

namespace ciphers {
    public class CaesarCipher{
        static public void Main(String[] args) {
            
            bool isInputValid = validateInput(args);
            // Do we have valid input
            if (!isInputValid) {
                Console.WriteLine("Invalid arguments passed to app");
                return;
            }
            
            // set up variables
            string inputText = args[0].ToLower();
            string direction = args[1];
            string directionWord = (direction=="E") ? "Encrypt" : "Decrypt";
            int shiftVal= int.Parse(args[2]);
            
            // perform the encryption/decription
            string output = cipher(inputText,shiftVal,direction);
            Console.WriteLine("Mode: " + directionWord);
            Console.WriteLine("shiftVal: " + shiftVal.ToString());
            Console.WriteLine("Input: " + inputText);
            Console.WriteLine("Output: " + output);

            
        }

        private static string cipher(string input, int shiftVal, string direction) {
            
            // get a list of the alphabet
            List<char> alphabet = getAlphabet();
            StringBuilder sb = new StringBuilder();

            //make the input string a list of characters and loop through it
            char[] chars  = input.ToCharArray();
            for (int i = 0; i < chars.Length; i++) {
                
                int adjustedPosition;
                int outputPosition;
                char currentChar = chars[i];
                
                // what if the message contains a non-alphabet character
                if (!alphabet.Contains(currentChar)) {
                    sb.Append(currentChar);
                }
                else {
                    // get the position in the alphabet of this character
                    int startingPosition = alphabet.IndexOf(currentChar);
                    
                    if (direction=="E") { 
                        // What if we transpose beyond Z - go back to the start of the alphabet
                        adjustedPosition = startingPosition + shiftVal;
                        outputPosition = (adjustedPosition>=26) ? adjustedPosition-26 : adjustedPosition;
                    }
                    else {
                        // What if we work back prior to a? - go to the end of the alphabet
                        adjustedPosition = startingPosition - shiftVal;
                        outputPosition = (adjustedPosition<0) ? adjustedPosition+26 : adjustedPosition;
                    }
                    
                    char outputChar = alphabet[outputPosition]; 
                    sb.Append(outputChar);
                }
            }

            return sb.ToString();

        }
        private static List<char> getAlphabet() {
            List<char> alphabet = new List<char>();
            alphabet.AddRange("abcdefghijklmnopqrstuvwxyz");
            return alphabet;
        }

        private static bool validateInput(String[] args) {
            if (args.Length != 3) {
                return false;
                }

            string direction = args[1];
            
            // Check Encrypt or Decrypt Flag
            if(direction !="E" && direction!="D") {
                return false;
            }

            // Check shift is a number
            string shift = args[2];
            bool isNumber = int.TryParse(shift, out int shiftVal);
            if (!isNumber) {
                return false;
            }
            
            if (shiftVal > 26) {
                Console.WriteLine("A tranposition value of > 26 is redundant - please only pass values 1-26");
                return false;
            }
            return true;
        }
    }
}