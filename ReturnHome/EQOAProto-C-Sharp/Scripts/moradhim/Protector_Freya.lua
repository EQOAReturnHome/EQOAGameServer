function  event_say(choice)
diagOptions = {}
    npcDialogue = "Mind yer step here in the Church of Below."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end