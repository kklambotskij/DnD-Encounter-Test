using UnityEngine;
using TMPro;

[System.Serializable]
public class Player : MonoBehaviour
{
    public string Name;
    public int InitiativeMod;
    public Damage damage;
    public int HP_max;
    public int AC;
    public int MOD;

    public int HP_current;
    public int turnOrderID;

    private TMP_InputField nameField;
    private TMP_InputField initiativeField;
    private TMP_Dropdown damageDiceCount;
    private TMP_Dropdown damageDiceType;
    private TMP_Dropdown damageMod;
    private TMP_InputField hpField;
    private TMP_InputField acField;
    private TMP_InputField modField;

    private void InitializeFields()
    {
        if (initiativeField == null)
        {
            nameField = transform.Find("Name").GetComponent<TMP_InputField>();
            initiativeField = transform.Find("Initiative").GetComponentInChildren<TMP_InputField>();


            var damage = transform.Find("Damage");
            damageDiceCount = damage.Find("Count").GetComponent<TMP_Dropdown>();
            damageDiceType = damage.Find("Dice").GetComponent<TMP_Dropdown>();
            damageMod = damage.Find("Modifier").GetComponent<TMP_Dropdown>();

            hpField = transform.Find("HP").GetComponentInChildren<TMP_InputField>();
            acField = transform.Find("AC").GetComponentInChildren<TMP_InputField>();
            modField = transform.Find("Mod").GetComponentInChildren<TMP_InputField>();
        }
    }

    public void LoadData()
    {
        InitializeFields();

        Name = nameField.text;
        InitiativeMod = int.Parse(initiativeField.text);

        damage.damageDiceCount = int.Parse(damageDiceCount.captionText.text);
        damage.damageDice = Rollable.StringToDice(damageDiceType.captionText.text);
        damage.damageMod = int.Parse(damageMod.captionText.text);

        HP_max = int.Parse(hpField.text);
        AC = int.Parse(acField.text);
        MOD = int.Parse(modField.text);

        HP_current = HP_max;
    }

    public void UploadData()
    {
        InitializeFields();

        nameField.text = Name;
        initiativeField.text = InitiativeMod.ToString();

        damageDiceCount.value = damage.damageDiceCount - 1;
        damageDiceType.value = (int)damage.damageDice;
        damageMod.value = damage.damageMod + 3;

        hpField.text = HP_max.ToString();
        acField.text = AC.ToString();
        modField.text = MOD.ToString();

        HP_current = HP_max;
    }

    public void UpdateCurrentHP()
    {
        if (hpField is null)
        {
            hpField = transform.Find("HP").GetComponentInChildren<TMP_InputField>();
        }
        hpField.text = HP_current.ToString();
    }

    public int RollForInitiative()
    {
        return Rollable.Roll(1, DiceType.d20) + InitiativeMod;
    }
}

[System.Serializable]
public class Damage : Rollable
{
    public int damageDiceCount;
    public DiceType damageDice;
    public int damageMod;

    public int Roll()
    {
        return Roll(damageDiceCount, damageDice) + damageMod;
    }
}
public enum DiceType { d4, d6, d8, d10, d12, d20, d100 }
public class Rollable
{
    public static int Roll(int count, DiceType dice)
    {
        if (count == 0)
        {
            return 0;
        }

        int result = 0;
        for (int i = 0; i < count; i++)
        {
            switch (dice)
            {
                case DiceType.d4:
                    result += Random.Range(1, 5);
                    break;
                case DiceType.d6:
                    result += Random.Range(1, 7);
                    break;
                case DiceType.d8:
                    result += Random.Range(1, 9);
                    break;
                case DiceType.d10:
                    result += Random.Range(1, 11);
                    break;
                case DiceType.d12:
                    result += Random.Range(1, 13);
                    break;
                case DiceType.d20:
                    result += Random.Range(1, 21);
                    break;
                case DiceType.d100:
                    result += Random.Range(1, 101);
                    break;
            }
        }
        return result;
    }
    public static DiceType StringToDice(string dice)
    {
        switch (dice)
        {
            case "d4":
                return DiceType.d4;
            case "d6":
                return DiceType.d6;
            case "d8":
                return DiceType.d8;
            case "d10":
                return DiceType.d10;
            case "d12":
                return DiceType.d12;
            case "d20":
                return DiceType.d20;
            case "d100":
                return DiceType.d100;
        }
        throw new System.Exception("StringToDice conversion error, input string: " + dice);
    }
}