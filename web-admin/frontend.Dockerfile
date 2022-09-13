# Base on offical Node.js Alpine image
FROM node:alpine

# Set working directory
WORKDIR /usr/app

COPY ./frontend/package.json ./frontend/yarn.lock ./
RUN yarn

COPY ./frontend .

# Build app
RUN yarn build

EXPOSE 3000

# Run container as non-root (unprivileged) user
# The node user is provided in the Node.js Alpine base image
USER node

CMD [ "yarn", "start" ]