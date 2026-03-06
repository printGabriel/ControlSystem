using System;
using System.Collections.Generic;
using System.Text;

namespace ControlSystem.Domain.Enums
{
    //enum responsável pela finalidade, se é receita, despesa ou ambos
    public enum PurposeType
    {
        Expense = 1,
        Income = 2,
        Both = 3
    }
}
