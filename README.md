## /api/Cat

### SaveFactsFromApi?amount={int}
Download given amount of Cat facts from external api and save them into .txt file

Returns
```
{
  "message": "Pomyślnie zapisano {int} faktów"
}
```
### /api/Cat/GetAllSavedCats
Return list of all saved facts in .txt file
```
[
  {
    "fact": "The Amur leopard is one of the most endangered animals in the world.",
    "length": 68,
    "downloadDate": "2024-11-14T10:59:33.4935734+01:00"
  },
  {
    "fact": "Cats only use their meows to talk to humans, not each other. The only time they meow to communicate with other felines is when they are kittens to signal to their mother.",
    "length": 170,
    "downloadDate": "2024-11-14T10:59:36.3405713+01:00"
  }
]
```

### /api/Cat/GetSavedCatsPage?page={int}&pageSize={int}
Return given amount of car facts

### /api/Cat/GetSavedFactsAmount
Returns value of saved records
