import React, { createContext, FC, ReactElement, useContext, useState } from 'react';

import axiosInstance from '../axiosConfig';

type ToDoContextType = {
  isLoggedIn: boolean;
  setLoggedIn: (loggedIn: boolean) => void;
  assigneeId: string | null;
  setAssigneeId: React.Dispatch<React.SetStateAction<string | null>>;
  tasks: any
  fetchTasks: () => void;
  getCompletionDate: () => void;
  setTasks: React.Dispatch<React.SetStateAction<never[]>>
  completionDate: Date | undefined;
};

const ToDoContext = createContext<ToDoContextType | undefined>(undefined);

export const useToDoContext = () => useContext(ToDoContext)!;

export const ContextProvider: FC<{ children: ReactElement }> = ({ children }): ReactElement => {
  const [isLoggedIn, setIsLoggedIn] = useState<boolean>(() => !!localStorage.getItem('assigneeId'));
  const [assigneeId, setAssigneeId] = useState(() => localStorage.getItem('assigneeId'));
  const [tasks, setTasks] = useState([]);
  const [completionDate, setCompletionDate] = useState<Date>();

  const fetchTasks = async () => {
    try {
      const response = await axiosInstance.get(`/tasks/assignee/${assigneeId}`);
      setTasks(response.data);
    } catch (error) {
      console.error('Error fetching tasks:', error);
    }
  };

  const getCompletionDate = async () => {
    try {
      const currentDate = new Date().toISOString();
      const completionTimeResponse = await axiosInstance.get(`/timeSheet/${assigneeId}/timesheet/${currentDate}`);
      const completionDateStr = completionTimeResponse.data;
      const parsedCompletionDate = new Date(completionDateStr);
      setCompletionDate(parsedCompletionDate);
    } catch (error) {
      console.error("Error fetching completion date:", error);
    }
  }

  const setLoggedIn = (loggedIn: boolean) => {
    if (loggedIn) {
      localStorage.setItem('assigneeId', 'true');
    } else {
      localStorage.removeItem('assigneeId');
    }
    setIsLoggedIn(loggedIn);
  };

  return (
    <ToDoContext.Provider value={{ isLoggedIn, setLoggedIn, assigneeId, setAssigneeId, tasks, fetchTasks, setTasks, getCompletionDate, completionDate }}>
      {children}
    </ToDoContext.Provider>
  );
};