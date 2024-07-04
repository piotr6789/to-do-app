import { FC } from 'react';

import { AppBar, Toolbar } from '@material-ui/core';

import Navigation from './navigation';

import { useService } from './service';

const Navbar: FC = (): JSX.Element => {
  const { classes } = useService();

  return (
    <AppBar {...{ className: classes.root }}>
      <Toolbar>
        <Navigation />
      </Toolbar>
    </AppBar>
  );
};

export default Navbar;