import { FC, useState } from 'react';

import { Button, Card, CardContent, MenuItem, Select, Typography } from '@material-ui/core';

import { useService } from './service';

const TaskBody: FC<{ id: number, description: string, status: number, title: string, estimateTime: number, currentAssigneeId: string, assignees: any }> =
({ id, description, status, title, estimateTime, currentAssigneeId, assignees }): JSX.Element => {
  const { classes, mapStatusToString, formatMinutesToHours, handleSaveClick } = useService();
  const [selectedAssigneeId, setSelectedAssigneeId] = useState<string>(currentAssigneeId);
  const [selectedStatus, setSelectedStatus] = useState<number>(status);
  
  const handleClick = async () => {
    await handleSaveClick(id, description, selectedStatus, title, estimateTime, selectedAssigneeId);
  };

  return (
    <Card {...{ className: classes.root }}>
      <CardContent>
        <Typography {...{ className: classes.title }}>{title}</Typography>
        <Typography>{description}</Typography>
        <Typography>Estimate time: {formatMinutesToHours(estimateTime)}</Typography>
        <Typography>Status: {mapStatusToString(selectedStatus)}</Typography>
        <Select
          value={selectedStatus}
          onChange={(e) => setSelectedStatus(e.target.value as number)}
          fullWidth
          variant="outlined">
          <MenuItem value={0}>TODO</MenuItem>
          <MenuItem value={1}>DONE</MenuItem>
        </Select>
        <Select
          value={selectedAssigneeId}
          onChange={(e) => setSelectedAssigneeId(e.target.value as string)}
          fullWidth
          variant="outlined">
          {assignees.map((assignee: any) => (
            <MenuItem key={assignee.id} value={assignee.id}>
              {assignee.name}
            </MenuItem>
          ))}
        </Select>
        <Button variant="contained" color="primary" onClick={handleClick}>Save</Button>
      </CardContent>
    </Card>
  );
};

export default TaskBody;