function  event_say(choice)
diagOptions = {}
    npcDialogue = "Those volcanos make me nervous. I wonder, what would happen to us if it ever erupted?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end