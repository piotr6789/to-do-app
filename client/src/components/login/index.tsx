import { FC, useState } from 'react';
import { Button, Card, CardContent, Select, MenuItem, Typography } from '@material-ui/core';
import { useService } from './service';

const Login: FC = (): JSX.Element => {
  const { classes, handleLogin, assignees } = useService();
  const [selectedAssigneeId, setSelectedAssigneeId] = useState<string | undefined>(undefined);

  const handleAssigneeChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    setSelectedAssigneeId(event.target.value as string);
  };

  return (
    <Card {...{ className: classes.root }}>
      <CardContent>
        <Typography {...{ className: classes.title }}>Login</Typography>
        <Select
          label="Select Assignee"
          variant="outlined"
          fullWidth
          value={selectedAssigneeId || ''}
          onChange={handleAssigneeChange}
          {...{ className: classes.textField }}
        >
          {assignees.map((assignee: any) => (
            <MenuItem key={assignee.id} value={assignee.id}>
              {assignee.name}
            </MenuItem>
          ))}
        </Select>
        <Button
          variant="contained"
          color="primary"
          onClick={() => handleLogin(selectedAssigneeId)}
          {...{ className: classes.button }}
        >
          Login
        </Button>
      </CardContent>
    </Card>
  );
};

export default Login;
