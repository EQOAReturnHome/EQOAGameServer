function  event_say(choice)
diagOptions = {}
    npcDialogue = "playerName, a power struggle is taking place within Klick`Anon. This necromancer Klockizar Sizzick, plotted to take over the city. Unfortunately for him, he was caught and put into jail."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end