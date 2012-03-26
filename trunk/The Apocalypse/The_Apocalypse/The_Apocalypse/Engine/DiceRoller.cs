using System;

namespace The_Apocalypse
{
        public class DiceRoller : IDisposable
        {

            private System.Security.Cryptography.RNGCryptoServiceProvider _Rngcsp;
            public DiceRoller()
            {
                _Rngcsp = new System.Security.Cryptography.RNGCryptoServiceProvider();
            }
            public int Roll(int nbFaces)
            {
                return Convert.ToInt16(RollDice(Convert.ToByte(nbFaces)));
            }
            public int Roll(int nbDices, int nbFaces)
            {
                int retVal = 0;
                for (int i = 0; i < nbDices; i++)
                {
                    retVal += Convert.ToInt16(RollDice(Convert.ToByte(nbFaces)));
                }
                return retVal;
            }
            private byte RollDice(byte numberSides)
            {

                // Create a new instance of the RNGCryptoServiceProvider.

                // Create a byte array to hold the random value.
                byte[] randomNumber = new byte[1];
                do
                {
                    // Fill the array with a random value.
                    _Rngcsp.GetBytes(randomNumber);
                }
                while (!IsFairRoll(randomNumber[0], numberSides));
                // Return the random number mod the number
                // of sides.  The possible values are zero-
                // based, so we add one.
                return (byte)((randomNumber[0] % numberSides) + 1);
            }

            private bool IsFairRoll(byte roll, byte numSides)
            {
                // There are MaxValue / numSides full sets of numbers that can come up
                // in a single byte.  For instance, if we have a 6 sided die, there are
                // 42 full sets of 1-6 that come up.  The 43rd set is incomplete.
                int fullSetsOfValues = Byte.MaxValue / numSides;

                // If the roll is within this range of fair values, then we let it continue.
                // In the 6 sided die case, a roll between 0 and 251 is allowed.  (We use
                // < rather than <= since the = portion allows through an extra 0 value).
                // 252 through 255 would provide an extra 0, 1, 2, 3 so they are not fair
                // to use.
                return roll < numSides * fullSetsOfValues;
            }
            public void Dispose()
            {
                _Rngcsp = null;
            }
        }
    }

