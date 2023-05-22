namespace EventSchedularProject.Models
{
    public class Exercise1
    {
        public static List<int> MakeSequence(int from, int end) // creating a list from 1 to 100
        {
            return Enumerable.Range(from, end - from + 1).ToList<int>();
        }
        //find even numbers in the list and print
        //public static void EvenNumbers(List<int> orgList)
        //{
        //  List<int> evenVals = orgList.Where(x => x % 2 == 0).ToList();
        //foreach (int eVal in evenVals)
        //{
        //  Console.Write(eVal + " ");
        //}
        //}
        public static List<int> EvenNumbers(List<int> orgList)
        {
            List<int> evenVals = orgList.Where(x => x % 2 == 0).ToList();
            return evenVals;
        }
        // divisible by 3 or 5 but not both
       // public static void Divide3or5butnotboth(List<int> orgList, int firstval, int secondval)
      //  {
            //foreach (int eVal in orgList.Where(x => ( x % firstval == 0 && x % secondval != 0) || (x % firstval != 0 && x % secondval == 0)))
            //foreach (int eVal in orgList)
           // {
             //   if ((eVal % firstval == 0 && eVal % secondval != 0) || (eVal % firstval != 0 && eVal % secondval == 0))
             //   {
             //       Console.Write(eVal + " ");
              //  }
           // }
        //}
        public static List<int> Divide3or5butnotboth(List<int> orgList, int firstval, int secondval)
        {
            //foreach (int eVal in orgList.Where(x => ( x % firstval == 0 && x % secondval != 0) || (x % firstval != 0 && x % secondval == 0)))
            List<int> newval = new List<int>();
            foreach (int eVal in orgList)
            {
                if ((eVal % firstval == 0 && eVal % secondval != 0) || (eVal % firstval != 0 && eVal % secondval == 0))
                {
                    newval.Add(eVal);
                }
            }
            return newval;
        }
    }
}
