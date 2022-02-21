﻿namespace Trash_Can.Controls
{
    /// <summary>
    /// State of <see cref="LoadingControl"/>
    /// </summary>
    public enum LoadingState
    {
        /// <summary> Normal </summary>
        None,

        /// <summary> Loading </summary>
        Loading,
        /// <summary> Load failed </summary>
        LoadFailed,

        /// <summary> File corrupt </summary>
        FileCorrupt,
        /// <summary> File null </summary>
        FileNull,

        /// <summary> Saving </summary>
        Saving,
        /// <summary> Save success </summary>
        SaveSuccess,
        /// <summary> Save failed </summary>
        SaveFailed,
    }
}