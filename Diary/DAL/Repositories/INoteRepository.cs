﻿using DiaryDAL.Entities;
using System.Collections.Generic;

namespace DiaryDAL.Repositories
{
    public interface INoteRepository
    {
        List<Note> GetNotes();
    }
}