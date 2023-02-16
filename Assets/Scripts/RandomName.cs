using UnityEngine;
using TMPro;

public class RandomName : MonoBehaviour
{

    private string[] firstNames = new string[] { "Adam", "Avery", "Bella", "Blake", "Cameron", "Carson", "Charlotte", "Chloe", "Christopher", "Claire", "Connor", "David", "Ella", "Emily", "Emma", "Ethan", "Grace", "Hailey", "Hannah", "Isabella", "Jack", "Jackson", "Jacob", "James", "Jasmine", "Jessica", "John", "Jordan", "Joseph", "Joshua", "Kaitlyn", "Katherine", "Kayla", "Lauren", "Liam", "Logan", "Madison", "Makayla", "Mason", "Matthew", "Megan", "Michael", "Morgan", "Natalie", "Nathan", "Nicholas", "Noah", "Olivia", "Parker", "Rachel", "Rebecca", "Riley", "Ryan", "Samantha", "Samuel", "Sarah", "Savannah", "Sophia", "Taylor", "Thomas", "Tyler", "Victoria", "William", "Wyatt", "Zachary", "Abraham", "Alexander", "Andrew", "Benjamin", "Charles", "Daniel", "Elizabeth", "Emma", "George", "Grace", "Hannah", "Henry", "Isaac", "Jacob", "James", "Jane", "John", "Joseph", "Joshua", "Liam", "Louis", "Lucas", "Luke", "Margaret", "Mary", "Mia", "Michael", "Oliver", "Olivia", "Peter", "Robert", "Samuel", "Sophia", "Thomas", "William" };

    private string[] lastNames = new string[] { "Smith", "Johnson", "Brown", "Taylor", "Miller", "Wilson", "Davis", "Clark", "Walker", "Allen", "Hall", "Young", "King", "Lee", "Wood", "Carter", "Garcia", "Martinez", "Jackson", "Lee", "Perez", "Turner", "Phillips", "Campbell", "Parker", "Evans", "Edwards", "Collins", "Stewart", "Sanchez", "Morris", "Rogers", "Peterson", "Cooper", "Reed", "Bailey", "Bell", "Gomez", "Kelly", "Howard", "Ward", "Cox", "Diaz", "Richardson", "Woodward", "Kim", "Cunningham", "James", "Scott", "Murphy", "Mitchell", "Graham", "Spencer", "Andrews", "Stevens", "Dixon", "Baker", "Brewer", "Foster", "Jordan", "Kennedy", "Watson", "Bryant", "Harris", "Mendoza", "Robinson", "Wright", "Chavez", "Rivera", "Murray", "Wells", "Willis", "Sullivan", "Ferguson", "Nichols", "Holland", "Shaw", "Barrett", "Carlson", "Armstrong", "Reyes", "Adams", "Simon", "Bates", "Franklin", "Grant", "Wagner", "Harrison", "Harper", "Snyder" };

    public TextMeshProUGUI nameText;

    private void Start()
    {
        string firstName = firstNames[Random.Range(0, firstNames.Length)];
        string lastName = lastNames[Random.Range(0, lastNames.Length)];
        string fullName = firstName + " " + lastName;
        gameObject.name = fullName;

        if (nameText != null)
        {
            nameText.SetText(fullName);
        }
    }
}