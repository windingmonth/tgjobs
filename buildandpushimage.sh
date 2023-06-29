#!/bin/bash 

pwd

docker version
docker build . -t tgjobs
docker tag tgjobs windingmonth/tgjobs

docker login -u windingmonth -p Xh7tce4ku
docker info

docker push windingmonth/tgjobs

echo 成功，按任意键继续
read -n 1
