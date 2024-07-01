if ( $psversiontable.PSVersion.Major -ige 2){
	Write-Debug "Starting the Powershell build script."
	Write-Debug "Powershell version: ($psversiontable.PSVersion)"
}else{
	Write-Debug " ($psversiontable.PSVersion) found instead"
	Write-Debug "Powershell version mis-match....Exiting"
	Write-Debug "Powershell version 2.0+ required"
	exit 1
}

$logFile = "E:\jenkins\workspace\build.log"
$errorcode=""

<#logging function#>
function mylog{
 Param($str)
 Write-Host (Date)": " $str | Tee-Object -file $logFile
}

mylog (pwd)
mylog (whoami)
$version = ""
<# Enables the debugging verbose output in log #>
mylog $args.count
mylog "version value is $($args[0])"

if ($args.count -eq 2){
	mylog "Argument number is correct"
    $version = "1.0.0.$($args[0])"
    mylog "version is: $($version)"
}else{
    mylog "Argument count not correct.  Please check script execution and try again"
    mylog "You should provide version number and release type(SNAPSHOT or Release) arguments"
    exit 1
}

$Service = "E:\jenkins\workspace\ContentManagement-master\CPS\Source\Production\Services"
mylog "Service is: $($Service)"
$webroot = "E:\jenkins\workspace\ContentManagement-master\CPS\Source\Production\WebRoot"
mylog "webroot is: $($webroot)"
$dirname = "E:\jenkins\workspace\ContentManagement-master\CPS\Source\Production\CPSElements-$($version)"
mylog "dirname is: $($dirname)"
$outputfile = "E:\jenkins\workspace\ContentManagement-master\CPS\Source\CPSElements-$($version).zip"
mylog "outputfile is: $($outputfile)"
$group = "com.hp.ContentManagement"
mylog "group is: $($group)"
$artifact = "CPSElements"
mylog "artifact is: $($artifact)"
$repoID = "Sabi-snapshot-M2"
mylog "repoID is: $($repoID)"
$url = "http://c0039921.itcs.hp.com:8080/nexus/content/repositories/Sabi-snapshot-M2"
mylog "url is: $($url)"
try 
{
    	New-Item $dirname -ItemType directory
		mylog "directory created"
        Move-Item $Service $dirname
		mylog "Service dir moved"
        Move-Item $webroot $dirname
		mylog "webroot directory moved"
		$mscript = { 
			param($dirname,$outputfile) 
			Write-Zip -Path $dirname -IncludeEmptyDirectories -OutputPath $outputfile
			}
        $job = Start-Job -ScriptBlock $mscript -ArgumentList $dirname, $outputfile
		Wait-Job $job
		Receive-Job $job
		$job | Remove-Job
		Start-Sleep -s 10
		mylog "created zip of $($dirname) output to $($outputfile)"
		if($args[1] -eq "SNAPSHOT"){
			$version = $version + "-SNAPSHOT"
			mylog "version is: $($version)"
		}else{
			$repoID = "Sabi-Release-M2"
			mylog "repoID is: $($repoID)"
			$url = "http://c0039921.itcs.hp.com:8080/nexus/content/repositories/Sabi-Release-M2"
			mylog "url is: $($url)"
		}
		mylog "-ArgumentList $($group), $($artifact), $($version), $($outputfile), $($repoID), $($url)"
		$mvnscript = { 
			param($group, $artifact, $version, $outputfile, $repoID, $url)
			write-host "Param: $($group), $($artifact), $($version), $($outputfile), $($repoID), $($url)"      
            $args1 = "-DgroupId=$($group)"
            $args2 = "-DartifactId=$($artifact)"
            $args3 = "-Dversion=$($version)"
            $args4 = "-Dpackaging=tar"
            $args5 = "-Dfile=$($outputfile)"
            $args6 = "-DrepositoryId=$($repoID)"
            $args7 = "-Durl=$($url)"
            
			mvn deploy:deploy-file -X $args1 $args2 $args3 $args4 $args5 $args6 $args7
		}
        $mvnresults = Start-Job -ScriptBlock $mvnscript -ArgumentList "$($group)", "$($artifact)", "$($version)", "$($outputfile)", "$($repoID)", "$($url)"
		Wait-Job $mvnresults
		Receive-Job $mvnresults
		$mvnresults | Remove-Job
		sleep -s 10
		mylog "mvn upload completed seems"
}
Catch [System.exception]
{
        $errorcode = $?
		mylog "Errorcode: $($errorcode)"
		exit 1
		
}

mylog "Package script completed with Exit code: $($errorcode)"
