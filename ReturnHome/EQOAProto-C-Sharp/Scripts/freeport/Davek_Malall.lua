function  event_say(choice)
diagOptions = {}
    npcDialogue = "Yes, the darkness touches the dagger and sword evenly. Only those truly embraced by the darkness shall know the truth behind life."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end