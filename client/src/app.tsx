import React from 'react';
import { Navigate, Route, Routes } from 'react-router-dom';

import Layout from './hoc/layout';
import Tasks from './components/tasks';
import Login from './components/login';
import RouteGuard from './route-guard';

import { MENU_ROUTES } from './constants/routes/routes';
import { ContextProvider } from './store/todo-context';

const App = () => (
    <ContextProvider>
      <Layout>
        <Routes>
          <Route path={MENU_ROUTES.LOGIN} element={<Login />} />
          <Route path={MENU_ROUTES.TASKS} element={<RouteGuard element={<Tasks />} />} />
          <Route path="*" element={<Navigate to={MENU_ROUTES.LOGIN} />} />
        </Routes>
      </Layout>
  </ContextProvider>
  );

export default App;
