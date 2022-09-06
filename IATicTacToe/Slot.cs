using System.ComponentModel;

namespace IATicTacToe;

public enum Slot
{
    [Description("X")]
    Cross,
    [Description("O")]
    Circle,
    [Description(" ")]
    None,
}