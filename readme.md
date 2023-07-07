>### DOCKER - Mongodb

* MongoDb: ```docker pull mongo```
* Create Container: ```docker run --name mongo_db -p 17017:27017 -d mongodb```

```docker run -d --name mongodb -p 27017:27017 -e MONGO_INITDB_ROOT_USERNAME=MyUser -e MONGO_INITDB_ROOT_PASSWORD=MyPassword mongo```