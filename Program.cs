using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace AdventOfCode {
	internal class Program {
		static void Main(string[] args) {
			//DayOne();
			DayTwo();


			


		}

		//===========[ DAY 1 ]==============
		public static void DayOne() {

			//Reading file content and splitting it up by spaces and newlines.
			string rawContent = String.Empty;
			StreamReader sr = new StreamReader("input.txt");
			rawContent = sr.ReadToEnd();
			string[] lineSplit = rawContent.Split(' ', '\n');

			//Making it so the list is just numbers.
			List<int> cleanList = new List<int>();
			for (int i = 0; i < lineSplit.Length; i++) {
				int toAdd;
				bool valid = int.TryParse(lineSplit[i], out toAdd);
				if (valid) {
					cleanList.Add(toAdd);
				}
			}

			//Creating the two seperate lists.
			List<int> numList1 = new List<int>();
			List<int> numList2 = new List<int>();
			bool addToListTwo = false;
			for (int a = 0; a < cleanList.Count; a++) {
				if (addToListTwo) {
					numList2.Add(cleanList[a]);
				} else {
					numList1.Add(cleanList[a]);
				}
				addToListTwo = !addToListTwo;
			}
			numList1.Sort();
			numList2.Sort();

			int answer = 0;
			for (int b = 0; b < numList2.Count; b++) {
				int total = numList1[b] - numList2[b];
				if (total < 0) { total = total * -1; }
				answer += total;
			}
			Console.WriteLine("Answer 1: " + answer.ToString());

			//Adding up each time a number appears on a list.
			List<int> simScores = new List<int>();
			for (int x = 0; x < numList1.Count; x++) {
				int reoccurance = 0;
				for (int y = 0; y < numList2.Count; y++) {
					if (numList1[x] == numList2[y]) {
						reoccurance++;
					}
				}
				simScores.Add(numList1[x] * reoccurance);
			}

			//Add up all simularity scores.
			int simTotal = 0;
			for (int p = 0; p < simScores.Count; p++) {
				simTotal += simScores[p];
			}
			Console.WriteLine("Answer 2: " + simTotal.ToString());
		}

		//===========[ DAY 2 ]==============
		public static async void DayTwo() {

			//Reading file content and splitting it up by spaces and newlines.
			string rawContent = String.Empty;
			StreamReader sr = new StreamReader("inputday2.txt");
			rawContent = sr.ReadToEnd();
			string[] reports = rawContent.Split('\n');

			int safeCount = 0;
			for (int i = 0; i < reports.Length; i++) {
				bool safe = true;

				//Create the list of levels based on the current line we are reading.
				string[] levelsString = reports[i].Split(" ");
				List<int> levels = new List<int>();
				for (int f = 0; f < levelsString.Length; f++) { levels.Add(int.Parse(levelsString[f])); }

				//first check if numbers are either all increasing or all decreasing.
				bool isIncreasing = IsIncreasingCheck(levels);
				bool isDecreasing = IsDecreasingCheck(levels);
				if (!isDecreasing && !isIncreasing) { safe = false; }

				bool diffFailed = false;
				if (isIncreasing) {
					for (int q = 1; q < levels.Count; q++) {
						int dist = levels[q] - levels[q - 1];
						if (dist < 0) { dist = dist * -1; }
						if (dist > 3) { diffFailed = true; }
					}
				} else {
					for (int q = 1; q < levels.Count; q++) {
						int dist = levels[q] - levels[q - 1];
						if (dist < 0) { dist = dist * -1; }
						if (dist > 3) { diffFailed = true; }
					}
				}

				if (diffFailed) { safe = false;	}


				if (safe) { safeCount++; }
			}
			Console.WriteLine("Answer: " + safeCount.ToString());
		}

		public static bool IsIncreasingCheck(List<int> nums) {
			for (int i = 0; i < nums.Count - 1; i++) {
				if (nums[i] >= nums[i + 1]) { return false;	}
			}
			return true;
		}

		public static bool IsDecreasingCheck(List<int> nums) {
			for (int i = 0; i < nums.Count - 1; i++) {
				if (nums[i] <= nums[i + 1]) { return false; }
			}
			return true;
		}
	}
}