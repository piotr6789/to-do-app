import { FC, useState } from 'react';
import { Button, Card, CardContent, MenuItem, Select, TextField } from '@material-ui/core';
import { useService } from './service';
import { useToDoContext } from '../../../store/todo-context';

interface NewTaskBodyProps {
  assignees: any[];
}

const NewTaskBody: FC<NewTaskBodyProps> = ({ assignees }): JSX.Element => {
  const { classes, handleSaveClick } = useService();
  const { fetchTasks, getCompletionDate } = useToDoContext();

  const [title, setTitle] = useState<string>('');
  const [description, setDescription] = useState<string>('');
  const [estimateTime, setEstimateTime] = useState<number>(0);
  const [status, setStatus] = useState<number>(0);
  const [selectedAssigneeId, setSelectedAssigneeId] = useState<string>(assignees.length ? assignees[0].id : '');

  const handleClick = async () => {
    await handleSaveClick(description, status, title, estimateTime, selectedAssigneeId);
    fetchTasks();
    getCompletionDate();
  };

  const handleKeyDown = (e: React.KeyboardEvent<HTMLInputElement>) => {
    if (e.key === 'ArrowUp') {
      setEstimateTime((prev) => prev + 30);
    } else if (e.key === 'ArrowDown') {
      setEstimateTime((prev) => (prev > 30 ? prev - 30 : prev));
    }
  };

  return (
    <Card className={classes.smallRoot}>
      <CardContent>
        <TextField
          label="Title"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          fullWidth
          variant="outlined"
          className={classes.smallTextField}
        />
        <TextField
          label="Description"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          fullWidth
          variant="outlined"
          className={classes.smallTextField}
        />
        <TextField
          label="Estimate Time"
          type="number"
          value={estimateTime}
          onKeyDown={handleKeyDown}
          fullWidth
          variant="outlined"
          className={classes.smallTextField}
        />
        <Select
          value={status}
          onChange={(e) => setStatus(e.target.value as number)}
          fullWidth
          variant="outlined"
          className={classes.smallSelect}
        >
          <MenuItem value={0}>TODO</MenuItem>
          <MenuItem value={1}>DONE</MenuItem>
        </Select>
        <Select
          value={selectedAssigneeId}
          onChange={(e) => setSelectedAssigneeId(e.target.value as string)}
          fullWidth
          variant="outlined"
          className={classes.smallSelect}
        >
          {assignees.map((assignee: any) => (
            <MenuItem key={assignee.id} value={assignee.id}>
              {assignee.name}
            </MenuItem>
          ))}
        </Select>
        <Button variant="contained" color="primary" onClick={handleClick}>
          Save
        </Button>
      </CardContent>
    </Card>
  );
};

export default NewTaskBody;
