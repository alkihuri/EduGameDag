using System;

namespace GameCore.Questions
{
    public static class QustionsAnswers  
    {
        public static int maxNumOfQust = 4;
        public static string questions = "Сколько сторон у 3-х угольника # Сколько сторон у 4-х угольника # Сколько сторон у 5-ти угольника # Сколько градусов у круга";
        public static string answers = "1,2,_3,4 # 1,2,3,_4 # 1,2,3,_5 # 1,2,_360,1";
        internal static bool gameIsOn = true;
        public static event Action OnScoreChange;
    }
}
