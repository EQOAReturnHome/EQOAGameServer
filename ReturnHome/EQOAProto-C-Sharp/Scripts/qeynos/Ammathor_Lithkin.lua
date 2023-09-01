function  event_say(choice)
diagOptions = {}
    npcDialogue = "As magicians, we must do what we can to help this great city prosper. Sometimes that means we must support House Bayle."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end