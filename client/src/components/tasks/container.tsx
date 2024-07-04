import { useEffect, useState } from 'react';

import axiosInstance from '../../axiosConfig';

import Grid from '@material-ui/core/Grid';
import TaskBody from './task-body';

import { useToDoContext } from '../../store/todo-context';
import NewTaskBody from './new-task-body';
import { FormControl, InputLabel, MenuItem, Select, Typography } from '@material-ui/core';

export const useContainer = () => {
  const context = useToDoContext();
  const [assignees, setAssignees] = useState([]);
  const [selectedStatus, setSelectedStatus] = useState<string>('');

  useEffect(() => {
    context.fetchTasks();
    context.getCompletionDate();
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

  useEffect(() => {
    const fetchTasksByStatus = async () => {
      try {
        const response = await axiosInstance.get(`/tasks/tasksByStatus/${context.assigneeId}/${selectedStatus}`);
        context.setTasks(response.data);
      } catch (error) {
        console.error("Error fetching tasks data:", error);
      }
    };
  
    if (selectedStatus) {
      fetchTasksByStatus();
    } else {
      context.fetchTasks();
    }
  }, [selectedStatus]);

  const content = context.tasks.length ? (
    <Grid container spacing={4}>
      <Grid item xs={12}>
        <FormControl variant="outlined" style={{ minWidth: 120 }}>
          <InputLabel>Status</InputLabel>
          <Select
            value={selectedStatus}
            onChange={(e) => setSelectedStatus(e.target.value as string)}
            label="Status"
          >
            <MenuItem value='0'>TODO</MenuItem>
            <MenuItem value='1'>DONE</MenuItem>
          </Select>
        </FormControl>
        <Typography>Estimate completion date: {context.completionDate ? context.completionDate.toLocaleString() : '-'}</Typography>

      </Grid>
      {context.tasks.map((task: any) => (
        <Grid item xs key={task.id}>
          <TaskBody
            {...{ id: task.id, description: task.description, status: task.status,
              title: task.title, estimateTime: task.estimateTime, currentAssigneeId: task.assigneeId, assignees }}
          />
        </Grid>
      ))}
      <NewTaskBody {...{assignees}}></NewTaskBody>
    </Grid>
  ) : (
    <NewTaskBody {...{assignees}}></NewTaskBody>
  );

  return { content };
};