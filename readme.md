>### DOCKER - Mongodb

* MongoDb: ```docker pull mongo```
* Create Container: ```docker run --name mongo_db -p 17017:27017 -d mongodb```

```docker run -d --name mongodb -p 27017:27017 -e MONGO_INITDB_ROOT_USERNAME=MyUser -e MONGO_INITDB_ROOT_PASSWORD=MyPassword mongo```

>## Processo de inclusão

Este sample possui uma implementação simples, uma lista para enviar 100 registros ao mongodb. Nesta lista, enviamos 99 linhas e a última possui uma alteração.

A ideia é demostrar como usar a operação de update e insert com uma chamada ao banco e garantir a consistência.
