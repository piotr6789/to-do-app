import React, { FC, ReactNode } from 'react';

import { Button } from '@material-ui/core';

import { useService } from './service';

const NavigationBtn: FC<{ route: string, children: ReactNode }> = ({ route, children })
  : JSX.Element => {
  const { classes, clickHandler } = useService(route);

  return (
    <Button {...{ className: classes.root, onClick: clickHandler }}>
      {children}
    </Button>
  );
};

export default NavigationBtn;