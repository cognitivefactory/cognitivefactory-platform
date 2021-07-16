using System;

namespace CognitiveFactory.Installer.Linux
{
    class K3d
    {
        // Executes : k3d version
        // Returns  : 
        // Version object if k3d.io version was correctly parsed
        // null otherwise
        public static Version CheckK3dVersion()
        {
            try
            {
                string output;
                string error;
                int exitcode = Process.Run("k3d", "version", 5, out output, out error);
                if (exitcode == 0 && String.IsNullOrEmpty(error) && !String.IsNullOrEmpty(output))
                {
                    // k3d version v4.4.7
                    // k3s version v1.21.2-k3s1 (default)
                    int versionIndex = output.IndexOf("version");
                    if (versionIndex > 0 && (versionIndex + 9) < output.Length)
                    {
                        versionIndex += 9;
                        int firstDot = output.IndexOf('.', versionIndex);
                        int secondDot = output.IndexOf('.', firstDot + 1);
                        int eol = output.IndexOf('\n', secondDot + 1);
                        if (firstDot > versionIndex && secondDot > firstDot && eol > secondDot)
                        {
                            var major = Int32.Parse(output.Substring(versionIndex, firstDot - versionIndex));
                            var minor = Int32.Parse(output.Substring(firstDot + 1, secondDot - firstDot - 1));
                            var build = Int32.Parse(output.Substring(secondDot + 1, eol - secondDot - 1));
                            return new Version(major, minor, build);
                        }
                    }
                }
            }
            catch (Exception)
            { }
            return null;
        }

        // Executes : k3d cluster list
        // Returns  : true if cluster already exists
        public static bool DoesK3dClusterExist(string clusterName)
        {
            try
            {
                string output;
                string error;
                int exitcode = Process.Run("k3d", "cluster list", 5, out output, out error);
                if (exitcode == 0 && String.IsNullOrEmpty(error) && !String.IsNullOrEmpty(output))
                {
                    // NAME                 SERVERS   AGENTS   LOADBALANCER
                    // cogfactory-cluster   1/1       3/3      true
                    var lines = output.Split('\n');
                    foreach(var line in lines)
                    {
                        if(line.StartsWith(clusterName))
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception)
            { }
            return false;
        }

        // Executes : k3d cluster create ...
        // Returns  : 
        // null is the cluster was sucessfully created
        // command string if the create command failed
        public static string CreateK3dCluster(string clusterName, int agents = 3, int hostWebPort = 8080, 
            bool createRegistry = true, bool exposeYDBPorts=true, bool mapHostPathDirectories=true, bool updateKubeconfig=true)
        {
            var command = $"cluster create {clusterName} --agents {agents} -p {hostWebPort}:80@loadbalancer";
            if (exposeYDBPorts)
            {
                command += " -p 7000:7000@loadbalancer -p 9042:9042@loadbalancer -p 6379:6379@loadbalancer -p 5433:5433@loadbalancer";
            }
            if (createRegistry)
            {
                command += " --registry-create";
            }
            if (mapHostPathDirectories)
            {
                for (int i = 0; i < agents; i++)
                {
                    command += $" --volume /var/lib/{clusterName}/storage/agent{i}:/var/lib/rancher/k3s/storage@agent[{i}]";
                }
            }
            if (updateKubeconfig)
            {
                command += " --kubeconfig-update-default=true";
            }
            try
            {
                string output;
                string error;                
                int exitcode = Process.Run("k3d", command, 300, out output, out error);
                if (exitcode == 0 && String.IsNullOrEmpty(error) && !String.IsNullOrEmpty(output))
                {
                    return null;
                }
            }
            catch (Exception)
            { }
            return command;
        }
    }
}
