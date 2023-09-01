function event_say()
diagOptions = {}
    npcDialogue = "Follow this coastline, and you will find our northern outpost. They help us keep an eye on things around Winter's Deep Lake."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end