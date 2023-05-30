﻿namespace BLang
{
    public partial class Parser
    {
        private enum eNonTerminal
        {
            File,
            Module,
            ModItem,
            ImportStatement,
            Function,
            VariableCreation,
            VariableInit,
            VariableDeclaration,
            OptionalType,
            Expression,
        }
    }
}