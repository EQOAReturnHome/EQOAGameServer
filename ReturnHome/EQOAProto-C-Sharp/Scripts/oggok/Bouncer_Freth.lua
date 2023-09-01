function  event_say(choice)
diagOptions = {}
    npcDialogue = "The ruins of Amog-Thelg are south of the city. Lately something has tainted those sacred grounds. playerName, it's better if you don't go there alone."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end