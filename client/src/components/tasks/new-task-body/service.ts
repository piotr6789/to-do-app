import axiosInstance from "../../../axiosConfig";

import useStyles from "./styles";
import { useToDoContext } from "../../../store/todo-context";

export const useService = () => {
  const classes = useStyles();
  const context = useToDoContext();

  const handleSaveClick = async (
    description: string,
    status: number,
    title: string,
    estimateTime: number,
    assigneeId: string
  ) => {
    try {
      const endpoint = `/tasks`;
      const data = {
        ...{ description, status, title, estimateTime, assigneeId },
      };

      await axiosInstance.post(endpoint, data);
      context.fetchTasks();
    } catch (error) {
      console.error("Error occured while updating task:", error);
    }
  };

  return { classes, handleSaveClick };
};
