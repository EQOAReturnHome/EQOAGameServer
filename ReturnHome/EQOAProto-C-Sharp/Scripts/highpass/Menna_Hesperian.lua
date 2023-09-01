function  event_say(choice)
diagOptions = {}
    npcDialogue = "While you don't appear to be absent of power, I still have no use for you.  Perhaps someone else can help you."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end