using Common;

namespace TheBroken
{
    [LegacyDoc(Order = "1.1", Caption = "Starting Stats", Content =
@"* Might: 1
* Lore: 3
* Intrigue: 2
* Command: 2
* Gold: 0  
  
Gets extra skill point upon creation.")]
    public static class Constants
    {
        public const int Might = 1;
        public const int Lore = 3;
        public const int Intrigue = 3;
        public const int Command = 2;
        public const int Gold = 0;
        public const int OnlyPerformedByDarkEmpire = -1;
    }
}