using System;
using System.Collections.Generic;
using PersonalOrganizerApp.Models;

namespace PersonalOrganizerApp.Models
{
    public class ProfileCaretaker
    {
        private Stack<User> undoStack = new Stack<User>();
        private Stack<User> redoStack = new Stack<User>();

        public void SaveState(User user)
        {
            undoStack.Push(user.Clone());
            redoStack.Clear();
        }

        public User Undo(User currentUser)
        {
            if (undoStack.Count == 0) return null;
            redoStack.Push(currentUser.Clone());
            User previous = undoStack.Pop();
            currentUser.CopyFrom(previous);
            return previous;
        }

        public User Redo(User currentUser)
        {
            if (redoStack.Count == 0) return null;
            undoStack.Push(currentUser.Clone());
            User next = redoStack.Pop();
            currentUser.CopyFrom(next);
            return next;
        }
    }

}

