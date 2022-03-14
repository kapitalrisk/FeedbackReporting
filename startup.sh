#!/bin/sh

docker build -t feedbackreporting .
docker run -it --rm -p 8000:80 --name feedbackreporting_app feedbackreporting