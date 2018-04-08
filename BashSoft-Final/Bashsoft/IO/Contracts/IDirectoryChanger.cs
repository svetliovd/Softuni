﻿namespace Bashsoft.IO.Contracts
{
    public interface IDirectoryChanger
    {
        void ChangeCurrentDirectoryRelative(string relativePath);

        void ChangeCurrentDirectoryAbsolute(string absolutePath);
    }
}
