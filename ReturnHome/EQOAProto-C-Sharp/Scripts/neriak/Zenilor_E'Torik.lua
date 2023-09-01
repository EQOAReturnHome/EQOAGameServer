function  event_say(choice)
diagOptions = {}
    npcDialogue = "Mazanda won't stop talking about the man she found down below by the gates to Darklight Palace. I, for one, am glad to be rid of him."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end