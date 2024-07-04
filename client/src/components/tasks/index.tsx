import { FC } from "react";

import { useContainer } from "./container";

const Tasks: FC = (): JSX.Element => {
  const { content } = useContainer();

  return content;
};

export default Tasks;
