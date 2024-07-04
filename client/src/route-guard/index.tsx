import React from 'react';
import { Navigate } from 'react-router-dom';

type RouteGuardProps = {
    element: React.ReactNode;
};

const RouteGuard: React.FC<RouteGuardProps> = ({ element }) => {
    const isLoggedIn = !!localStorage.getItem('assigneeId');

    if (isLoggedIn) {
        return <>{element}</>;
    } else {
        return <Navigate to={'login'} />;
    }
};

export default RouteGuard;
