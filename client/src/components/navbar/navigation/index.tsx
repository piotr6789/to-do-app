import { FC } from 'react';

import NavBtn from './navigation-btn';

import { MENU_ROUTES } from '../../../constants/routes/routes';

const Navigation: FC = (): JSX.Element => {
  return (
    <>
      <NavBtn {...{ route: MENU_ROUTES.LOGIN }}>Login</NavBtn>
      <NavBtn {...{ route: MENU_ROUTES.TASKS }}>Tasks</NavBtn>
    </>
  );
};

export default Navigation;