function  event_say(choice)
diagOptions = {}
    npcDialogue = "Don't get too many strangers passing through Hodstock. Just as well, they usually bring trouble along with them."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end