import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

import axiosInstance from "../../axiosConfig";

import useStyles from "./styles";
import { useToDoContext } from "../../store/todo-context";

export const useService = () => {
  const classes = useStyles();
  const [assignees, setAssignees] = useState([]);
  const [, setId] = useState("");
  const navigate = useNavigate();
  const context = useToDoContext();

  useEffect(() => {
    const storedId = localStorage.getItem("assigneeId");
    if (storedId) {
      setId(storedId);
    }
  }, []);

  useEffect(() => {
    const fetchAssignees = async () => {
      try {
        const response = await axiosInstance.get("/assignees");
        setAssignees(response.data);
      } catch (error) {
        console.error("Error fetching tasks:", error);
      }
    };
    fetchAssignees();
  }, []);

  const handleLogin = (selectedAssigneeId: string | undefined) => {
    if (!selectedAssigneeId) {
      console.error("Please select an assignee.");
      return;
    }
    localStorage.setItem("assigneeId", String(selectedAssigneeId));
    context.setAssigneeId(selectedAssigneeId);
    navigate("/tasks");
  };

  return { classes, handleLogin, assignees };
};
