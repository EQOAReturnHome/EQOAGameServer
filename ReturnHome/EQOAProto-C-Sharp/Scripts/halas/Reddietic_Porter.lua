function  event_say(choice)
diagOptions = {}
    npcDialogue = "Nothing to worry about. I haven't had an accident...reported...in over a week. Now, where do you want me to send you?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end