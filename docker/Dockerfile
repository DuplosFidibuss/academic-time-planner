FROM nginx:latest
ARG PAT
ARG VERSION=latest
RUN apt-get update
RUN apt-get install unzip -y 
RUN apt install jq -y
RUN rm -rf /etc/nginx/conf.d/default.conf
COPY nginx/default.conf /etc/nginx/conf.d/default.conf
RUN rm -rf /usr/share/nginx/html/*
RUN if [ $VERSION != 'latest' ]; then VERSION="tags/$VERSION"; fi \
    && ASSET_URL="$( \
    curl -H "Authorization: token $PAT" -s "https://api.github.com/repos/BAIT-versttob-eggersi1/Academic-Time-Planner/releases/$VERSION" | jq -r '.assets[0].url' \
    )" \
    && curl -vLJO -H "Authorization: token $PAT" -H 'Accept: application/octet-stream' $ASSET_URL
RUN unzip Academic-Time-Planner.zip -d Academic-Time-Planner
RUN mv Academic-Time-Planner/wwwroot/* /usr/share/nginx/html