import { FC, ReactNode } from 'react';

import Navbar from '../../components/navbar';

import { useService } from './service';

const Layout: FC<{ children: ReactNode }> = ({ children }): JSX.Element => {
  const { classes } = useService();

  return (
    <>
      <Navbar />
      <main {...{ className: classes.root }}>{children}</main>
    </>
  );
};

export default Layout;