using System.Linq;

namespace Days {
    public class Day3 : DaySolverBase {
        public override string Example1 =>
            @"vJrwpWtwJgWrhcsFMMfFFhFp
jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
PmmdzqPrVvPwwTWBwg
wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
ttgJtRGJQctTZtZT
CrZsJsPPZsGzwwsLwLmpwMDw";

        List<string> TransformToList(string raw) {
            List<string> stringList = new List<string>();
           stringList = raw.Split("\n").ToList<string>();
            return stringList;
        }

        public override object Solve1(string raw) {
            List<string> stringList = TransformToList(raw);

            List<char> sharedItems = new List<char>();

            foreach (var ruckSackString in stringList)
            {
            char sharedItem = GetSharedItem(ruckSackString);
                sharedItems.Add(sharedItem);
            }

            int prioSum = 0;

            foreach (var sharedItemChar in sharedItems) {
                int itemPrio = GetItemValue(sharedItemChar);
               prioSum += itemPrio;
            }
            return prioSum;
        }

        private static int GetItemValue(char sharedItemChar) {
            int itemPrio = (int)sharedItemChar;
            if (itemPrio >= 65 && itemPrio <= 90)
            {
                return itemPrio - 38;
            }
            else
            {
                return itemPrio - 96;
            }
        }

        private static char GetSharedItem(string ruckSackString) {
            char[] firstRuckSack = ruckSackString.ToArray<char>();
            int itemAmount = firstRuckSack.Count<char>();
            char[] firstHalf = new char[itemAmount / 2];
            char[] secondHalf = new char[itemAmount / 2];
            for(int i = 0; i < itemAmount / 2; i++) {
                firstHalf[i] = firstRuckSack[i];
                secondHalf[i] = firstRuckSack[i + itemAmount / 2];
            }

            char sharedItem = '1';
            foreach(char item in firstHalf) {
                if(secondHalf.Contains<char>(item)) {
                    sharedItem = item;
                }
            }

            return sharedItem;
        }

        public override object Solve2(string raw) {
            List<string> stringList = TransformToList(raw);

            List<List<string>> allGroupsOfSacks = new List<List<string>>();

            int count = 0;
            for (int i = 0; i < stringList.Count/3; i++)
            {
                List<string> groupOfSacks = new List<string>();

                for (int j = 0; j < 3; j++)
                {
                groupOfSacks.Add(stringList[i * 3 + j]);
                }
                allGroupsOfSacks.Add(groupOfSacks);
            }



            List<char> sharedItems = new List<char>();

            foreach (List<string> groupOfSacks in allGroupsOfSacks) {
                char sharedItem = GetSharedItemInGroup(groupOfSacks);
                sharedItems.Add(sharedItem);
            }

            int prioSum = 0;
            foreach (var sharedItemChar in sharedItems)
            {
                int itemPrio = GetItemValue(sharedItemChar);
                prioSum += itemPrio;
            }
            return prioSum;
        }

        private static char GetSharedItemInGroup(List<string> groupOfSacks) {
            List<char> itemsSharedBetweenFirstAndSecond = new List<char>();
            foreach (var itemInFirstSack in groupOfSacks[0])
            {
                if (groupOfSacks[1].Contains<char>(itemInFirstSack))
                {
                    itemsSharedBetweenFirstAndSecond.Add(itemInFirstSack);
                }
            }

            foreach (var itemInFirstAndSecond in itemsSharedBetweenFirstAndSecond)
            {
                if (groupOfSacks[2].Contains<char>(itemInFirstAndSecond))
                {
                    return itemInFirstAndSecond;
                }
            }
            return '1';
        }
    }
}