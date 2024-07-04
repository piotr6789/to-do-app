import useStyles from "./styles";

import axiosInstance from "../../../axiosConfig";

import { TaskStatus } from "../../../constants/TaskStatus";
import { useToDoContext } from "../../../store/todo-context";

export const useService = () => {
  const classes = useStyles();
  const context = useToDoContext();

  const handleSaveClick = async (
    id: number,
    description: string,
    status: number,
    title: string,
    estimateTime: number,
    assigneeId: string
  ) => {
    try {
      const endpoint = `/tasks/${id}`;
      const data = {
        ...{ id, description, status, title, estimateTime, assigneeId },
      };

      await axiosInstance.put(endpoint, data);
      context.fetchTasks();
      context.getCompletionDate();
    } catch (error) {
      console.error("Error occured while updating task:", error);
    }
  };

  function mapStatusToString(status: TaskStatus): string {
    switch (status) {
      case TaskStatus.TODO:
        return "TODO";
      case TaskStatus.DONE:
        return "DONE";
      default:
        return "";
    }
  }

  function formatMinutesToHours(minutes: number): string {
    const hours = Math.floor(minutes / 60);
    const remainingMinutes = minutes % 60;

    return `${hours}.${remainingMinutes}h`;
  }

  return { classes, mapStatusToString, formatMinutesToHours, handleSaveClick };
};
