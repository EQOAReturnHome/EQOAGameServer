function event_say()
diagOptions = {}
    npcDialogue = "It seems like more and more adventurers are coming to visit Connor and Elmo these days. I've questioned them both, but naturally they won't tell me a thing."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end